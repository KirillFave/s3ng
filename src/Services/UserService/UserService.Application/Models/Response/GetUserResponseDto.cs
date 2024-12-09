using UserService.Models.Results;

namespace UserService.Models.Response;

public class GetUserResponseDto
{
    public UserDto User { get; set; }
    public GetUserResultModel Result { get; set; }
}