using AutoMapper;
using MediatR;
using UserService.EFCore.Entities;
using UserService.Models;
using UserService.Models.Requests;
using UserService.Models.Response;
using UserService.Models.Results;
using UserService.Repository;

namespace UserService.Handler;

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
            var newUser = await _repository.AddAsync(new User
            {
                Id = Guid.NewGuid(),
                AuthenticationId = Guid.Parse(request.AuthenticationId),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.PhoneNumber,
                Address = request.Address,
                Role = Converter.ConvertRoleToEntity(request.Role),
                CreatedAt = DateTime.UtcNow
            }, cancellationToken);

            if (newUser == null)
            {
                createUserResponseDto.Result = CreateUserResultModel.Fail;
                return createUserResponseDto;
            }

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
        finally
        {
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}