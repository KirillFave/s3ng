using Grpc.Core;
using IAM.DAL;
using IAM.Entities;
using IAM.Seedwork.Abstractions;
using Microsoft.EntityFrameworkCore;
using s3ng.Contracts.IAM;
using ILogger = Serilog.ILogger;

namespace IAM.Services
{
    internal sealed class RegistrationService : Registration.RegistrationBase
    {
        private readonly ILogger _logger;
        private readonly DatabaseContext _databaseContext;
        private readonly IHashCalculator _hashCalculator;

        public RegistrationService(DatabaseContext dbContext
            , ILogger logger
            , IHashCalculator hashCalculator) 
        {
            _databaseContext = dbContext;
            _logger = logger.ForContext<RegistrationService>();
            _hashCalculator = hashCalculator;
        }

        public override async Task<RegisterResponse> RegisterUser(RegisterRequest request, ServerCallContext context)
        {
            _logger.Information($"Получили запрос на регистрацию юзера Login:{request.Login}");

            if (request is null)
            {
                _logger.Warning(nameof(request) + " is null");
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Login))
            {
                _logger.Warning(nameof(request.Login) + " is null");
                throw new ArgumentNullException(nameof(request.Login));
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                _logger.Warning(nameof(request.Password) + " is null");
                throw new ArgumentNullException(nameof(request.Password));
            }

            var isAlreadyExists = (await _databaseContext.Users.FirstOrDefaultAsync(x => x.Login == request.Login, cancellationToken: context.CancellationToken)) is not null;
            if (isAlreadyExists)
            {
                var textError = $"{request.Login} is exists";
                _logger.Error(textError);
                return new RegisterResponse() { Message = textError, Result = RegisterResult.Fail };
            }
            else
            {
                _logger.Information($"Успешно отработали запрос на регистрацию юзера Login:{request.Login}");
                var passwordHash = _hashCalculator.Compute(request.Password);
                var newId = Guid.NewGuid();
                var newUser = new User { Id = newId, Login = request.Login, PasswordHash = passwordHash };
                await _databaseContext.Users.AddAsync(newUser, cancellationToken: context.CancellationToken);
                await _databaseContext.SaveChangesAsync();
                return new RegisterResponse() { Message = newId.ToString(), Result = RegisterResult.Success };
            }
        }
    }
}
