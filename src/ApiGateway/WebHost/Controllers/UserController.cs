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

    [HttpGet("{id}")]
    public async Task<GetUserResponse> GetAsync(string id)
    {
        var request = new GetUserRequest { Id = id };
        return await _client.GetUserAsync(request);
    }

    [HttpPost]
    public async Task<CreateUserResponse> CreateAsync(UserInfo userInfo)
    {
        var request = new CreateUserRequest { User = userInfo };
        return await _client.CreateUserAsync(request);
    }
}