using MediatR;
using UserService.Models.Response;

namespace UserService.Models.Requests;

public class DeleteUserRequestDto : IRequest<DeleteUserResponseDto>
{
    public required Guid Id { get; set; }
}