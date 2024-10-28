using AutoMapper;
using MediatR;
using UserService.Models;
using UserService.Models.Requests;
using UserService.Models.Response;
using UserService.Models.Results;
using UserService.Repository;

namespace UserService.Handler;

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
        catch (Exception ex)
        {
            getUserResponseDto.Result = GetUserResultModel.Fail;
            return getUserResponseDto;
        }
    }
}
