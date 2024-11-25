using MediatR;
using UserService.Models.Requests;
using UserService.Models.Response;
using UserService.Repository;

namespace UserService.Handler;

public class GetUserByAuthenticationIdHandler : IRequestHandler<GetUserByAuthenticationIdRequestDto, GetUserResponseDto>
{
    private readonly IUserRepository _repository;
    public GetUserByAuthenticationIdHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetUserResponseDto> Handle(GetUserByAuthenticationIdRequestDto request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}