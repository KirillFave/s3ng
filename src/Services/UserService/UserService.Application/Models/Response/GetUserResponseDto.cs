using SharedLibrary.UserService.Models;
using UserService.Application.Models.Results;

namespace UserService.Application.Models.Response;

public class GetUserResponseDto
{
    public UserModel User { get; set; }
    public GetUserResultModel Result { get; set; }
}
