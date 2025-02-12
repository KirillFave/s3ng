using System.Net;
using AutoMapper;
using FluentValidation;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using s3ng.Contracts.IAM;
using WebHost.Dto.IAM;
using ILogger = Serilog.ILogger;

namespace WebHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class IAMController(ILogger logger
            , Registration.RegistrationClient registrationClient
            , Authentication.AuthenticationClient authenticationClient
            , IMapper mapper
            , IValidator<AuthenticationRequestDto> authenticationValidator
            , IValidator<RegistrationRequestDto> registrationValidator
            , IWebHostEnvironment webHostEnvironment) : ControllerBase
    {
        private readonly ILogger _logger = logger.ForContext<IAMController>();
        private readonly Registration.RegistrationClient _registrationClient = registrationClient;
        private readonly Authentication.AuthenticationClient _authenticationClient = authenticationClient;
        private readonly IValidator<AuthenticationRequestDto> _authenticationValidator = authenticationValidator;
        private readonly IValidator<RegistrationRequestDto> _registrationValidator = registrationValidator;
        private readonly IMapper _mapper = mapper;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        /// <summary>
        /// Зарегистрировать пользователя
        /// </summary>
        /// <param name="requestDto">Запрос</param>
        /// <param name="ct">Токен отмены</param>
        [HttpPost("/Register/")]
        [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser(RegistrationRequestDto requestDto, CancellationToken ct)
        {
            try
            {
                _logger.Information($"Api method {nameof(RegisterUser)} was called with parameters {requestDto.Email}, {requestDto.Password}");
                var validationResult = _registrationValidator.Validate(requestDto);
                if (!validationResult.IsValid)
                    return new BadRequestObjectResult(validationResult.Errors);

                requestDto.Role = _webHostEnvironment.IsDevelopment()
                    ? requestDto.Role ?? SharedLibrary.IAM.Enums.RoleType.User
                        : SharedLibrary.IAM.Enums.RoleType.User;

                var serviceRequest = _mapper.Map<RegisterRequest>(requestDto);

                var result = await _registrationClient.RegisterUserAsync(serviceRequest, cancellationToken: ct);
                _logger.Information($"end call registration with result {result.Result}, {result.Message}");
                return result.Result switch
                {
                    RegisterResult.Success => new OkObjectResult(result.Message),
                    _ => new BadRequestObjectResult(result.Message)
                };
            }
            catch (RpcException ex)
            {
                _logger.Error($"gRPC Error: {ex.Status.Detail}");
                throw;
            }
        }


        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="requestDto">Запрос</param>
        /// <param name="ct">Токен отмены</param>
        [HttpPost("/Auth/")]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthenticationUser(AuthenticationRequestDto requestDto, CancellationToken ct)
        {
            _logger.Information($"Api method {nameof(AuthenticationUser)} was called with parameters {requestDto.Email}, {requestDto.Password}");
            var validationResult = _authenticationValidator.Validate(requestDto);
            if (!validationResult.IsValid)
                return new BadRequestObjectResult(validationResult.Errors);

            var serviceRequest = _mapper.Map<AuthenticationRequest>(requestDto);
            var result = await _authenticationClient.AuthenticateUserAsync(serviceRequest, cancellationToken: ct);
            _logger.Information($"end call registration with result {result.Result}, {result.Token}");
            if (result.Result == AuthenticationResult.Success)
            {
                HttpContext.Response.Cookies.Append("drugs", result.Token);
                return new OkObjectResult(result.Token);
            }

            return result.Result switch
            {
                AuthenticationResult.BadPassword =>
                    new UnauthorizedObjectResult("Bad password"),

                AuthenticationResult.UserNotFound =>
                    new NotFoundObjectResult("User not found"),

                _ =>
                    StatusCode((int)HttpStatusCode.InternalServerError)
            };
        }
    }
}
