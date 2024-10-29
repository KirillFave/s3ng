using MediatR;
using UserService.Models.Response;

namespace UserService.Models.Requests;

public class GetUserByAuthenticationIdRequestDto : IRequest<GetUserResponseDto>
{
    public required Guid AuthenticationId { get; set; }
}