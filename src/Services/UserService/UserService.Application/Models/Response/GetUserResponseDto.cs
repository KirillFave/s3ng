using UserService.Application.Models.Results;

namespace UserService.Application.Models.Response;

public class GetUserResponseDto
{
    public UserDto User { get; set; }
    public GetUserResultModel Result { get; set; }
}