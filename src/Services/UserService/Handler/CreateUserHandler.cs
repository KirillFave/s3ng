using MediatR;
using UserService.Models.Requests;
using UserService.Models.Response;
using UserService.Repository;

namespace UserService.Handler;

public class CreateUserHandler : IRequestHandler<CreateUserRequestDto, CreateUserResponseDto>
{
    private readonly IUserRepository _repository;
    public CreateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateUserResponseDto> Handle(CreateUserRequestDto request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}