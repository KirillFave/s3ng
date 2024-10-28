using MediatR;
using UserService.Models.Requests;
using UserService.Models.Response;
using UserService.Repository;

namespace UserService.Handler;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequestDto, GetAllUsersResponseDto>
{
    private readonly IUserRepository _repository;
    public GetAllUsersHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetAllUsersResponseDto> Handle(GetAllUsersRequestDto request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}