using SharedLibrary.UserService.Models;
using UserService.Application.Models.Results;

namespace UserService.Application.Models.Response;

public class UpdateUserResponseDto
{
    public UserModel User { get; set; }
    public UpdateUserResultModel Result { get; set; }
}
