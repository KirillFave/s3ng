using UserService.Application.Models.Results;

namespace UserService.Application.Models.Response;

public class UpdateUserResponseDto
{
    public UserDto User { get; set; }
    public UpdateUserResultModel Result { get; set; }
}