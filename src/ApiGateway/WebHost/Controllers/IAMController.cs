using System.Net;
using AutoMapper;
using FluentValidation;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
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
            const string apiMethodName = nameof(RegisterUser);
            try
            {
                _logger.Information($"Api method {apiMethodName} was called with parameters {requestDto.Email}, {requestDto.Password}");
                var validationResult = _registrationValidator.Validate(requestDto);
                if (!validationResult.IsValid)
                    return new BadRequestObjectResult(validationResult.Errors);

                requestDto.Role = _webHostEnvironment.IsDevelopment()
                    ? requestDto.Role ?? SharedLibrary.IAM.Enums.RoleType.User
                        : SharedLibrary.IAM.Enums.RoleType.User;

                var serviceRequest = _mapper.Map<RegisterRequest>(requestDto);

                var result = await _registrationClient.RegisterUserAsync(serviceRequest, cancellationToken: ct);
                _logger.Information($"end call {apiMethodName} with result {result.Result}, {result.Message}");

                return result.Result switch
                {
                    RegisterResult.Success => Ok(new { Message = result.Message }),
                    _ => BadRequest(new { Message = result.Message })
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
            const string apiMethodName = nameof(AuthenticationUser);

            _logger.Information($"Api method {apiMethodName} was called with parameters {requestDto.Email}, {requestDto.Password}");
            var validationResult = _authenticationValidator.Validate(requestDto);
            if (!validationResult.IsValid)
                return new BadRequestObjectResult(validationResult.Errors);

            var serviceRequest = _mapper.Map<AuthenticationRequest>(requestDto);
            var result = await _authenticationClient.AuthenticateUserAsync(serviceRequest, cancellationToken: ct);
            _logger.Information($"end call {apiMethodName} with result {result.Result}, {result.Token}");

            if (result.Result == AuthenticationResult.AuthSuccess)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,  // TODO при https сменить на true
                    SameSite = SameSiteMode.Strict
                };

                HttpContext.Response.Cookies.Append("jwt-token-cookie", result.RefreshToken, cookieOptions);

                return Ok(new
                {
                    AccessToken = result.Token
                });
            }

            return result.Result switch
            {
                AuthenticationResult.AuthInvalidPassword => Unauthorized(new { Message = "Invalid password" }),
                AuthenticationResult.AuthUserNotFound => NotFound(new { Message = "User not found" }),
                _ => StatusCode((int)HttpStatusCode.InternalServerError)
            };
        }

        /// <summary>
        /// Обновить токен
        /// </summary>
        /// <param name="ct">Токен отмены</param>
        [HttpPost("/Refresh/")]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken(CancellationToken ct)
        {
            const string apiMethodName = nameof(RefreshToken);

            if (!Request.Cookies.TryGetValue("jwt-token-cookie", out var token))
            {
                return Unauthorized(new { message = "RefreshToken не найден" });
            }

            _logger.Information($"Api method {apiMethodName} was called");
            var serviceRequest = _mapper.Map<RefreshAccessTokenRequest>(new RefreshTokenRequestDto(token));
            var result = await _authenticationClient.RefreshAccessTokenAsync(serviceRequest, cancellationToken: ct);
            _logger.Information($"end call {apiMethodName} with result {result.Result}, {result.Token}");

            if (result.Result == RefreshTokenResult.RefreshSuccess)
            {
                HttpContext.Response.Cookies.Delete("jwt-token-cookie");

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false, // TODO при https сменить на true
                    SameSite = SameSiteMode.Strict
                };

                HttpContext.Response.Cookies.Append("jwt-token-cookie", result.RefreshToken, cookieOptions);

                return Ok(new
                {
                    AccessToken = result.Token
                });
            }

            return result.Result switch
            {
                RefreshTokenResult.RefreshInvalidToken => BadRequest(new { Message = "Invalid token" }),
                RefreshTokenResult.RefreshUserNotFound => NotFound(new { Message = "User not found" }),
                _ => StatusCode((int)HttpStatusCode.InternalServerError, new { Message = "Internal Server Error" })
            };
        }

        /// <summary>
        /// Выйти из системы
        /// </summary>
        [HttpPost("/Logout/")]
        public IActionResult Logout()
        {
            const string apiMethodName = nameof(Logout);

            _logger.Information($"Api method {apiMethodName} was called");
            HttpContext.Response.Cookies.Delete("jwt-token-cookie");

            _logger.Information($"end call {apiMethodName}");
            return Ok(new { message = "Logged out successfully" });
        }

        /// <summary>
        /// Получить профиль юзера
        /// </summary>
        [HttpGet("profile")]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> GetProfile(CancellationToken ct)
        {
            const string apiMethodName = nameof(GetProfile);

            if (!Request.Cookies.TryGetValue("jwt-token-cookie", out var token))
            {
                return Forbid();
            }

            _logger.Information($"Api method {apiMethodName} was called");
            var serviceRequest = _mapper.Map<GetProfileRequest>(new GetProfileRequestDto(token));
            var result = await _authenticationClient.GetProfileAsync(serviceRequest, cancellationToken: ct);
            _logger.Information($"end call {apiMethodName} with result {result.Result}, {result.Email}");

            if (result.Result == GetProfileResult.GetProfileSuccess)
            {
                return Ok(new
                {
                    Email = result.Email,
                    AuthenticationId = result.AuthenticationId
                });
            }

            return result.Result switch
            {
                GetProfileResult.GetProfileInvalidToken => BadRequest(new { Message = "Invalid token" }),
                GetProfileResult.GetProfileUserNotFound => NotFound(new { Message = "User not found" }),
                _ => StatusCode((int)HttpStatusCode.InternalServerError, new { Message = "Internal Server Error" })
            };
        }
    }
}
