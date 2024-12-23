using UserService.Application.Models.Results;

namespace UserService.Application.Models.Response;

public class CreateUserResponseDto
{
    public UserDto User { get; set; }
    public CreateUserResultModel Result { get; set; }
}