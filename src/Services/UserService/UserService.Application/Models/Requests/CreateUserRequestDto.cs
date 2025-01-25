using MediatR;
using SharedLibrary.UserService.Models;
using UserService.Application.Models.Response;

namespace UserService.Application.Models.Requests;

public class CreateUserRequestDto : IRequest<CreateUserResponseDto>
{
    public required string AuthenticationId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long Phone { get; set; }
    public string Address { get; set; }
    public RoleModel Role { get; set; }
}
