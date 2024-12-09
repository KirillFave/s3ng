using MediatR;
using AutoMapper;
using UserService.Domain.Entities;
using UserService.Infrastructure.Repository;
using UserService.Models;
using UserService.Models.Requests;
using UserService.Models.Response;
using UserService.Models.Results;

namespace UserService.Handler;

public class UpdateUserHandler : IRequestHandler<UpdateUserRequestDto, UpdateUserResponseDto>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UpdateUserHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UpdateUserResponseDto> Handle(UpdateUserRequestDto request, CancellationToken cancellationToken)
    {
        var updateUserResponseDto = new UpdateUserResponseDto();
        try
        {
            var user = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                updateUserResponseDto.Result = UpdateUserResultModel.NotFound;
                return updateUserResponseDto;
            }

            var updateUser = await _repository.UpdateAsync(GetUpdateUser(request, user), cancellationToken);

            if (updateUser == null)
            {
                updateUserResponseDto.Result = UpdateUserResultModel.Fail;
                return updateUserResponseDto;
            }

            await _repository.SaveChangesAsync(cancellationToken);

            var userDto = _mapper.Map<UserDto>(updateUser);
            updateUserResponseDto.User = userDto;
            updateUserResponseDto.Result = UpdateUserResultModel.Success;
            return updateUserResponseDto;
        }
        catch
        {
            updateUserResponseDto.Result = UpdateUserResultModel.Fail;
            return updateUserResponseDto;
        }
    }

    private User GetUpdateUser(UpdateUserRequestDto dto, User oldUser)
    {
        return new User()
        {
            Id = dto.Id,
            AuthenticationId = oldUser.AuthenticationId,
            FirstName = dto.FirstName ?? oldUser.FirstName,
            LastName = dto.LastName ?? oldUser.LastName,
            Role = oldUser.Role,
            Address = dto.Address ?? oldUser.Address,
            Phone = dto.PhoneNumber > 0 ? dto.PhoneNumber : oldUser.Phone,
            CreatedAt = oldUser.CreatedAt
        };
    }
}