using MediatR;
using UserService.Models.Requests;
using UserService.Models.Response;
using UserService.Repository;

namespace UserService.Handler;

public class UpdateUserHandler : IRequestHandler<UpdateUserRequestDto, UpdateUserResponseDto>
{
    private readonly IUserRepository _repository;
    public UpdateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateUserResponseDto> Handle(UpdateUserRequestDto request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}