using Grpc.Core;
using IAM.DAL;
using IAM.Seedwork.Abstractions;
using Microsoft.EntityFrameworkCore;
using s3ng.Contracts.IAM;
using ILogger = Serilog.ILogger;

namespace IAM.Services
{
    internal sealed class AuthenticationService : Authentication.AuthenticationBase
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger _logger;
        private readonly IHashCalculator _hashCalculator;
        private readonly ITokenProvider _tokenProvider;

        public AuthenticationService(DatabaseContext dbContext
            , ILogger logger
            , IHashCalculator hashCalculator
            , ITokenProvider tokenProvider)
        {
            _dbContext = dbContext;
            _logger = logger.ForContext<AuthenticationService>();
            _hashCalculator = hashCalculator;
            _tokenProvider = tokenProvider;
        }

        public override async Task<AuthenticationResponse> AuthenticateUser(AuthenticationRequest request, ServerCallContext context)
        {
            _logger.Information($"Получили запрос на аутентификацию юзера Login:{request.Login}");

            try
            {
                //Получаем пользователя по логину
                var user = await _dbContext.Users
                    .FirstOrDefaultAsync(x => x.Login == request.Login, cancellationToken: context.CancellationToken);
                if (user is null)
                {
                    _logger.Error($"Invalid userName {request.Login}");
                    return new AuthenticationResponse() { Result = AuthenticationResult.UserNotFound };
                }
                //Сравниваем хэш
                var passwordHash = _hashCalculator.Compute(request.Password);
                if (!passwordHash.Equals(user.PasswordHash, StringComparison.OrdinalIgnoreCase))
                {
                    _logger.Error($"Invalid hash for {request.Password}. Expected {user.PasswordHash}, value = {passwordHash}");
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
                return new AuthenticationResponse() { Result = AuthenticationResult.Fail };
            }
        }
    }
}
