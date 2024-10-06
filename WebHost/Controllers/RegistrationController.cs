using Microsoft.AspNetCore.Mvc;
using s3ng.Contracts.IAM;

namespace s3ng.WebHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly Registration.RegistrationClient _registrationClient;

        public RegistrationController(ILogger<RegistrationController> logger, Registration.RegistrationClient registrationClient)
        {
            _logger = logger;
            _registrationClient = registrationClient;
        }

        [HttpPost]
        [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registration(string login, string password, CancellationToken ct)
        {
            _logger.LogInformation($"calling registration");
            var result = await _registrationClient.RegisterUserAsync(new RegisterRequest { Login = login, Password = password },
                cancellationToken: ct);
            _logger.LogInformation($"end call registration");
            return result.Success ? new OkObjectResult(result.Message) : new BadRequestObjectResult(result.Message);
        }
    }
}