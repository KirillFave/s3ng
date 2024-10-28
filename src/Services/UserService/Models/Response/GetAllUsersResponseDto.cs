using UserService.Models.Results;

namespace UserService.Models.Response;

public class GetAllUsersResponseDto
{
    public List<UserDto> Users { get; set; }
    public GetAllUsersResultModel Result { get; set; }
}
