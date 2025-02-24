using AutoMapper;
using Grpc.Core;
using IAM.Models;
using IAM.Repositories;
using IAM.Seedwork.Abstractions;
using Microsoft.AspNetCore.Identity;
using s3ng.Contracts.IAM;
using ILogger = Serilog.ILogger;

namespace IAM.Services
{
    internal sealed class AuthenticationService(UserRepository userRepository
            , RefreshTokenRepository refreshTokenRepository
            , ILogger logger
            , ITokenProvider tokenProvider
            , IMapper mapper
            ) : Authentication.AuthenticationBase
    {
        private readonly UserRepository _repository = userRepository;
        private readonly RefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
        private readonly ILogger _logger = logger.ForContext<AuthenticationService>();
        private readonly ITokenProvider _tokenProvider = tokenProvider;
        private readonly IMapper _mapper = mapper;

        public override async Task<AuthenticationResponse> AuthenticateUser(AuthenticationRequest request, ServerCallContext context)
        {
            _logger.Information($"Получили запрос на аутентификацию юзера {nameof(request.Email)}:{request.Email}");

            try
            {
                //Получаем пользователя по логину
                var user = await _repository.GetByEmailAsync(request.Email, context.CancellationToken);
                if (user is null)
                {
                    _logger.Error($"User with {nameof(request.Email)} {request.Email} not exists");
                    return new AuthenticationResponse() { Result = AuthenticationResult.AuthUserNotFound };
                }

                var account = _mapper.Map<AccountModel>(user);
                var verifyResult = new PasswordHasher<AccountModel>().VerifyHashedPassword(account, account.PasswordHash, request.Password);
                if (verifyResult == PasswordVerificationResult.Failed)
                {
                    _logger.Error($"Invalid password");
                    return new AuthenticationResponse() { Result = AuthenticationResult.AuthInvalidPassword };
                }

                //Вычисляем токен доступа и генерируем токен обновления
                var token = _tokenProvider.GenerateToken(user);
                var refreshToken = _tokenProvider.GenerateRefreshToken();

                //Сохраняем рефреш токен
                await _refreshTokenRepository.SaveRefreshToken(refreshToken, user.Id);

                _logger.Information($"Успешно прошли аутентификацию для юзера с {nameof(request.Email)}:{request.Email}");

                return new AuthenticationResponse()
                {
                    Result = AuthenticationResult.AuthSuccess,
                    Token = token,
                    RefreshToken = refreshToken
                };
            }
            catch (Exception ex)
            {
                _logger.Error("Во время аутентификации получили ошибку", ex);
                return new AuthenticationResponse() { Result = AuthenticationResult.AuthUnspecified };
            }
        }

        public override async Task<RefreshAccessTokenResponse> RefreshAccessToken(RefreshAccessTokenRequest request, ServerCallContext context)
        {
            _logger.Information($"Получили запрос на обновление accessToken");

            try
            {
                var userId = await _refreshTokenRepository.ValidateRefreshToken(request.RefreshToken);
                if (userId is null)
                {
                    _logger.Error($"Invalid refresh token");
                    return new RefreshAccessTokenResponse() { Result = RefreshTokenResult.RefreshInvalidToken };
                }

                var user = await _repository.GetByIdAsync(userId.Value, context.CancellationToken);
                if (user is null)
                {
                    _logger.Error($"User not found");
                    return new RefreshAccessTokenResponse() { Result = RefreshTokenResult.RefreshUserNotFound };
                }

                var newAccessToken = _tokenProvider.GenerateToken(user);
                var newRefreshToken = _tokenProvider.GenerateRefreshToken();

                //Обновляем в хранилище токен
                await _refreshTokenRepository.RemoveRefreshToken(request.RefreshToken);
                await _refreshTokenRepository.SaveRefreshToken(newRefreshToken, user.Id);

                return new RefreshAccessTokenResponse()
                {
                    Result = RefreshTokenResult.RefreshSuccess,
                    Token = newAccessToken,
                    RefreshToken = newRefreshToken
                };
            }
            catch (Exception ex)
            {
                _logger.Error("Ошибка при обновлении токена", ex);
                return new RefreshAccessTokenResponse() { Result = RefreshTokenResult.RefreshUnspecified };
            }
        }
    }
}
