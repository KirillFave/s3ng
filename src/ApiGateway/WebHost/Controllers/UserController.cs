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
        var request = new GetUserByIdRequest { Id = id };
        return await _client.GetUserByIdAsync(request);
    }
}