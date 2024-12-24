using System.Net;
using AutoMapper;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using s3ng.Contracts.IAM;
using WebHost.Dto.IAM;
using ILogger = Serilog.ILogger;

namespace WebHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class IAMController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly Registration.RegistrationClient _registrationClient;
        private readonly Authentication.AuthenticationClient _authenticationClient;
        private readonly IMapper _mapper;

        public IAMController(ILogger logger
            , Registration.RegistrationClient registrationClient
            , Authentication.AuthenticationClient authenticationClient
            , IMapper mapper)
        {
            _registrationClient = registrationClient;
            _authenticationClient = authenticationClient;
            _mapper = mapper;
            _logger = logger.ForContext<IAMController>();
        }

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
                _logger.Information($"Api method {nameof(RegisterUser)} was called with parameters {requestDto.Login}, {requestDto.Password}");
                var serviceRequest = _mapper.Map<RegisterRequest>(requestDto);
                var result = await _registrationClient.RegisterUserAsync(serviceRequest, cancellationToken: ct);
                _logger.Information($"end call registration with result {result.Result}, {result.Message}");
                return result.Result == RegisterResult.Success ? new OkObjectResult(result.Message) : new BadRequestObjectResult(result.Message);
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
            _logger.Information($"Api method {nameof(AuthenticationUser)} was called with parameters {requestDto.Login}, {requestDto.Password}");
            var serviceRequest = _mapper.Map<AuthenticationRequest>(requestDto);
            var result = await _authenticationClient.AuthenticateUserAsync(serviceRequest, cancellationToken: ct);
            _logger.Information($"end call registration with result {result.Result}, {result.Token}");
            switch (result.Result)
            {
                case AuthenticationResult.Success:
                    {
                        HttpContext.Response.Cookies.Append("drugs", result.Token);
                        return new OkObjectResult(result.Token);
                    }
                case AuthenticationResult.BadPassword:
                    return new UnauthorizedObjectResult("Bad password");
                case AuthenticationResult.UserNotFound:
                    return new NotFoundObjectResult("User not found");
                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
