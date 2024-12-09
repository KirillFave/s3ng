using UserService.Models.Results;

namespace UserService.Models.Response;

public class CreateUserResponseDto
{
    public UserDto User { get; set; }
    public CreateUserResultModel Result { get; set; }
}