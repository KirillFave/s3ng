using MediatR;
using UserService.Application.Models.Response;

namespace UserService.Application.Models.Requests;

public class CreateUserRequestDto : IRequest<CreateUserResponseDto>
{
    public required string AuthenticationId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public long PhoneNumber { get; set; }
    public required string Address { get; set; }
    public required RoleDto Role { get; set; }
}