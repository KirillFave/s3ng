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
    internal sealed class AuthenticationService(UserRepository userRepository,
            ILogger logger,
            ITokenProvider tokenProvider,
            IMapper mapper) : Authentication.AuthenticationBase
    {
        private readonly UserRepository _repository = userRepository;
        private readonly ILogger _logger = logger.ForContext<AuthenticationService>();
        private readonly ITokenProvider _tokenProvider = tokenProvider;
        private readonly IMapper _mapper = mapper;

        public override async Task<AuthenticationResponse> AuthenticateUser(AuthenticationRequest request, ServerCallContext context)
        {
            _logger.Information($"Получили запрос на аутентификацию юзера Login:{request.Login}");

            try
            {
                //Получаем пользователя по логину
                var user = await _repository.GetByLoginAsync(request.Login, context.CancellationToken);
                if (user is null)
                {
                    _logger.Error($"Invalid userName {request.Login}");
                    return new AuthenticationResponse() { Result = AuthenticationResult.UserNotFound };
                }

                var account = _mapper.Map<AccountModel>(user);
                var verifyResult = new PasswordHasher<AccountModel>().VerifyHashedPassword(account, account.PasswordHash, request.Password);
                if (verifyResult == PasswordVerificationResult.Failed)
                {
                    _logger.Error($"Invalid password");
                    return new AuthenticationResponse() { Result = AuthenticationResult.BadPassword };
                }

                //Вычисляем и возвращаем токен
                var token = _tokenProvider.GenerateToken(user);

                _logger.Information($"Успешно прошли аутентификацию для юзера с Login:{request.Login}");

                return new AuthenticationResponse() { Result = AuthenticationResult.Success, Token = token };
            }
            catch (Exception ex)
            {
                _logger.Error("Во время аутентификации получили ошибку", ex);
                return new AuthenticationResponse() { Result = AuthenticationResult.Unspecified };
            }
        }
    }
}
