using MediatR;
using UserService.Models.Requests;
using UserService.Models.Response;
using UserService.Repository;

namespace UserService.Handler;

public class DeleteUserHandler : IRequestHandler<DeleteUserRequestDto, DeleteUserResponseDto>
{
    private readonly IUserRepository _repository;
    public DeleteUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteUserResponseDto> Handle(DeleteUserRequestDto request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
