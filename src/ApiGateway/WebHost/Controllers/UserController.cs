using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.IAM.Enums;
using SharedLibrary.UserService.Models;
using static CreateUserResponse.Types;
using static DeleteUserResponse.Types;
using static GetUserResponse.Types;
using static UpdateUserResponse.Types;
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
        _logger = logger.ForContext<UserController>();
        _mapper = mapper;
    }

    /// <summary>
    /// Получение юзера по ID
    /// </summary>
    [HttpGet("get{id}")]
    [ProducesResponseType<UserModel>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserAsync(string id)
    {
        _logger.Information("Api method GetUserAsync was called with parameters {id}", id);
        var request = new GetUserByIdRequest { Id = id };

        var result = await _client.GetUserByIdAsync(request);
        _logger.Information("end call GetUserAsync with result {result}", result.Result);

        if (result.Result != GetUserResult.Success)
            return new BadRequestObjectResult(result.Result);

        var userInfo = _mapper.Map<UserModel>(result.User);
        return new OkObjectResult(userInfo);
    }

    /// <summary>
    /// Создание нового юзера
    /// </summary>
    [HttpPost("create")]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUserAsync(CreateUserRequestModel request)
    {
        _logger.Information("Api method CreateUserAsync was called with name: {FirstName}, {LastName}", request.FirstName, request.LastName);
        var createUserRequest = _mapper.Map<CreateUserRequest>(request);
        var result = await _client.CreateUserAsync(createUserRequest);

        _logger.Information("end call CreateUserAsync with result {result}", result.Result);

        if (result.Result != CreateUserResult.Success)
            return new BadRequestObjectResult(result.Result);

        return new OkObjectResult(result.User.Id);
    }

    /// <summary>
    /// Редактирование информации о юзере
    /// </summary>
    [HttpPut("edit")]
    [ProducesResponseType<UserModel>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = nameof(RoleType.User) + "," + nameof(RoleType.Admin))] // Разрешение для обеих ролей
    public async Task<IActionResult> UpdateUserAsync(UpdateUserRequestModel request)
    {
        _logger.Information("Api method UpdateUserAsync was called");
        var updateUserRequest = _mapper.Map<UpdateUserRequest>(request);
        var result = await _client.UpdateUserAsync(updateUserRequest);

        _logger.Information("end call UpdateUserAsync with result {result}", result.Result);

        if (result.Result != UpdateUserResult.Success)
            return new BadRequestObjectResult(result.Result);

        var userInfo = _mapper.Map<UserModel>(result.User);
        return new OkObjectResult(userInfo);
    }

    /// <summary>
    /// Удаление юзера
    /// </summary>
    [HttpDelete("delete{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = nameof(RoleType.Admin))]
    public async Task<IActionResult> DeleteUserAsync(string id)
    {
        _logger.Information("Api method DeleteUserAsync was called with parameters {id}", id);
        var request = new DeleteUserRequest { Id = id };

        var result = await _client.DeleteUserAsync(request);
        _logger.Information("end call DeleteUserAsync with result {result}", result.Result);

        if (result.Result != DeleteUserResult.Success)
            return new BadRequestObjectResult(result.Result);

        return new OkObjectResult(true);
    }
}
