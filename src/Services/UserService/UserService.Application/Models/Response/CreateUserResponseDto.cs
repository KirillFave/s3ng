using SharedLibrary.UserService.Models;
using UserService.Application.Models.Results;

namespace UserService.Application.Models.Response;

public class CreateUserResponseDto
{
    public UserModel User { get; set; }
    public CreateUserResultModel Result { get; set; }
}
