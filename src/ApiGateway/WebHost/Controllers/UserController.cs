using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebHost.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager.UserManagerClient _client;

    public UserController(UserManager.UserManagerClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Получение юзера по ID
    /// </summary>
    [HttpGet("{id}")]
    //[Authorize]
    public async Task<GetUserResponse> GetUserAsync(string id)
    {
        var request = new GetUserByIdRequest { Id = id };
        return await _client.GetUserByIdAsync(request);
    }

    /// <summary>
    /// Создание нового юзера
    /// </summary>
    [HttpPost("create")]
    public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request)
    {
        return await _client.CreateUserAsync(request);
    }
}
