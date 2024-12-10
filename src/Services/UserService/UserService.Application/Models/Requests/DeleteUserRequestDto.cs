using MediatR;
using UserService.Application.Models.Response;

namespace UserService.Application.Models.Requests;

public class DeleteUserRequestDto : IRequest<DeleteUserResponseDto>
{
    public required Guid Id { get; set; }
}