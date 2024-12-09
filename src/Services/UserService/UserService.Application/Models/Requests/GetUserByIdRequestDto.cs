using MediatR;
using UserService.Models.Response;

namespace UserService.Models.Requests;

public class GetUserByIdRequestDto : IRequest<GetUserResponseDto>
{
    public required Guid Id { get; set; }
}