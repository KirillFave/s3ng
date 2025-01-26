using AutoMapper;
using Grpc.Core;
using IAM.DAL;
using IAM.Entities;
using IAM.Models;
using IAM.Producers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using s3ng.Contracts.IAM;
using SharedLibrary.Common.Kafka.Messages;
using ILogger = Serilog.ILogger;

namespace IAM.Services
{
    internal sealed class RegistrationService : Registration.RegistrationBase
    {
        private readonly ILogger _logger;
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        private readonly UserRegistredProducer _userRegistredProducer;

        public RegistrationService(DatabaseContext dbContext, ILogger logger, IMapper mapper, UserRegistredProducer userRegistredProducer) 
        {
            _databaseContext = dbContext;
            _logger = logger.ForContext<RegistrationService>();
            _mapper = mapper;
            _userRegistredProducer = userRegistredProducer;
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
                var newId = Guid.NewGuid();
                var newAccount = new AccountModel()
                { 
                    Id = newId,
                    Login = request.Login,
                };

                var passwordHash = new PasswordHasher<AccountModel>().HashPassword(newAccount, request.Password);
                newAccount.PasswordHash = passwordHash;

                var newUser = _mapper.Map<User>(newAccount);
                newUser.Role = SharedLibrary.IAM.Enums.RoleType.User;

                //TODO нужен отдельный класс для работы с BD
                await _databaseContext.Users.AddAsync(newUser, cancellationToken: context.CancellationToken);
                await _userRegistredProducer.ProduceAsync(newId.ToString(), new UserRegistredMessage
                {
                    RegistredAt = DateTimeOffset.UtcNow,
                    Id = newId.ToString()
                }, context.CancellationToken);

                await _databaseContext.SaveChangesAsync();

                _logger.Information($"Успешно отработали запрос на регистрацию юзера Login:{request.Login}");
                return new RegisterResponse() { Message = newId.ToString(), Result = RegisterResult.Success };
            }
        }
    }
}
