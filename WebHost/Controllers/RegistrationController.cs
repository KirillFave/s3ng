using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using s3ng.Contracts.IAM;
using s3ng.WebHost.Dto;

namespace s3ng.WebHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly Registration.RegistrationClient _registrationClient;
        private readonly IMapper _mapper;

        public RegistrationController(ILogger<RegistrationController> logger, Registration.RegistrationClient registrationClient, IMapper mapper)
        {
            _logger = logger;
            _registrationClient = registrationClient;
            _mapper = mapper;
        }

        /// <summary>
        /// Зарегистрировать поользователя
        /// </summary>
        /// <param name="requestDto">Запрос</param>
        /// <param name="ct">Токен отмены</param>
        [HttpPost]
        [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser(RegistrationRequestDto requestDto, CancellationToken ct)
        {
            _logger.LogInformation($"Api method {nameof(RegisterUser)} was called with parameters {requestDto.Login}, {requestDto.Password}");
            var serviceRequest = _mapper.Map<RegisterRequest>(requestDto);
            var result = await _registrationClient.RegisterUserAsync(serviceRequest, cancellationToken: ct);
            _logger.LogInformation($"end call registration with result {result.Success}, {result.Message}");
            return result.Success ? new OkObjectResult(result.Message) : new BadRequestObjectResult(result.Message);
        }
    }
}