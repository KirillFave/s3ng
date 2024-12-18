using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebHost.Dto.User;
using static CreateUserResponse.Types;
using static GetUserResponse.Types;
using ILogger = Serilog.ILogger;

namespace WebHost.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager.UserManagerClient _client;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public UserController(UserManager.UserManagerClient client, ILogger logger, IMapper mapper)
    {
        _client = client;
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Получение юзера по ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserAsync(string id)
    {
        _logger.Information($"Api method {nameof(GetUserAsync)} was called with parameters {id}");
        var request = new GetUserByIdRequest { Id = id };

        var result = await _client.GetUserByIdAsync(request);
        _logger.Information($"end call GetUserAsync with result {result.Result}");

        if (result.Result != GetUserResult.Success)
            return new BadRequestObjectResult(result.Result);

        var userInfo = _mapper.Map<UserDto>(result.User);
        return new OkObjectResult(result.User);
    }

    /// <summary>
    /// Создание нового юзера
    /// </summary>
    [HttpPost("create")]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUserAsync(CreateUserRequestDto request)
    {
        _logger.Information($"Api method {nameof(CreateUserAsync)} was called with name: {request.FirstName}, {request.LastName}");
        var createUserRequest = _mapper.Map<CreateUserRequest>(request);
        var result = await _client.CreateUserAsync(createUserRequest);

        _logger.Information($"end call CreateUserAsync with result {result.Result}");

        if (result.Result != CreateUserResult.Success)
            return new BadRequestObjectResult(result.Result);

        return new OkObjectResult(result.User.Id);
    }
}
