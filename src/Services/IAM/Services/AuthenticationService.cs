using Grpc.Core;
using IAM.DAL;
using IAM.Seedwork.Abstractions;
using Microsoft.EntityFrameworkCore;
using s3ng.Contracts.IAM;

namespace IAM.Services
{
    internal sealed class AuthenticationService : Authentication.AuthenticationBase
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IHashCalculator _hashCalculator;
        private readonly ITokenProvider _tokenProvider;

        public AuthenticationService(DatabaseContext dbContext
            , ILogger<AuthenticationService> logger
            , IHashCalculator hashCalculator
            , ITokenProvider tokenProvider)
        {
            _dbContext = dbContext;
            _logger = logger;
            _hashCalculator = hashCalculator;
            _tokenProvider = tokenProvider;
        }

        public override async Task<AuthenticationResponse> AuthenticateUser(AuthenticationRequest request, ServerCallContext context)
        {
            //Получаем пользователя по логину
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Login == request.Login, cancellationToken: context.CancellationToken);
            if (user is null)
            {
                _logger.LogError($"Invalid userName {request.Login}");
                return new AuthenticationResponse() { Result = AuthenticationResult.UserNotFound };
            }
            //Сравниваем хэш
            var passwordHash = _hashCalculator.Compute(request.Password);
            if (!passwordHash.Equals(user.PasswordHash, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogError($"Invalid hash for {request.Password}. Expected {user.PasswordHash}, value = {passwordHash}");
                return new AuthenticationResponse() { Result = AuthenticationResult.BadPassword };
            }

            //Вычисляем и возвращаем токен
            var token = _tokenProvider.GenerateToken(user);
            return new AuthenticationResponse() { Result = AuthenticationResult.Success, Token = token };
        }
    }
}
