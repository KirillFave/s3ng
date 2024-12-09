using UserService.Models.Results;

namespace UserService.Models.Response;

public class UpdateUserResponseDto
{
    public UserDto User { get; set; }
    public UpdateUserResultModel Result { get; set; }
}