using MediatR;
using UserService.Application.Models.Response;

namespace UserService.Application.Models.Requests;

public class GetUserByIdRequestDto : IRequest<GetUserResponseDto>
{
    public required Guid Id { get; set; }
}