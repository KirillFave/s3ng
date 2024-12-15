using AutoMapper;
using MediatR;
using UserService.Domain.Entities;
using UserService.Infrastructure.Repository;
using UserService.Application.Models;
using UserService.Application.Models.Requests;
using UserService.Application.Models.Response;
using UserService.Application.Models.Results;

namespace UserService.Application.Handler;

public class CreateUserHandler : IRequestHandler<CreateUserRequestDto, CreateUserResponseDto>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public CreateUserHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CreateUserResponseDto> Handle(CreateUserRequestDto request, CancellationToken cancellationToken)
    {
        var createUserResponseDto = new CreateUserResponseDto();
        try
        {
            var userEntity = _mapper.Map<User>(request);

            var newUser = await _repository.AddAsync(userEntity, cancellationToken);

            if (newUser == null)
            {
                createUserResponseDto.Result = CreateUserResultModel.Fail;
                return createUserResponseDto;
            }

            await _repository.SaveChangesAsync(cancellationToken);

            var userDto = _mapper.Map<UserDto>(newUser);
            createUserResponseDto.User = userDto;
            createUserResponseDto.Result = CreateUserResultModel.Success;
            return createUserResponseDto;
        }
        catch
        {
            createUserResponseDto.Result = CreateUserResultModel.Fail;
            return createUserResponseDto;
        }
    }
}