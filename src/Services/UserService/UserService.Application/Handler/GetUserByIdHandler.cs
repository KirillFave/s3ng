using AutoMapper;
using MediatR;
using UserService.Infrastructure.Repository;
using UserService.Application.Models;
using UserService.Application.Models.Requests;
using UserService.Application.Models.Response;
using UserService.Application.Models.Results;

namespace UserService.Application.Handler;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequestDto, GetUserResponseDto>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetUserResponseDto> Handle(GetUserByIdRequestDto request, CancellationToken cancellationToken)
    {
        var getUserResponseDto = new GetUserResponseDto();

        try
        {
            var user = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                getUserResponseDto.Result = GetUserResultModel.NotFound;
                return getUserResponseDto;
            }

            var userDto = _mapper.Map<UserDto>(user);
            getUserResponseDto.Result = GetUserResultModel.Success;
            getUserResponseDto.User = userDto;
            return getUserResponseDto;
        }
        catch
        {
            getUserResponseDto.Result = GetUserResultModel.Fail;
            return getUserResponseDto;
        }
    }
}
