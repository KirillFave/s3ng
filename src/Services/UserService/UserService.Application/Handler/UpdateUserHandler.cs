using MediatR;
using AutoMapper;
using UserService.Domain.Entities;
using UserService.Infrastructure.Repository;
using UserService.Application.Models;
using UserService.Application.Models.Requests;
using UserService.Application.Models.Response;
using UserService.Application.Models.Results;
using System.Net;

namespace UserService.Application.Handler;

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

    private User GetUpdateUser(UpdateUserRequestDto dto, User user)
    {
        user.FirstName = string.IsNullOrEmpty(dto.FirstName) ? user.FirstName : dto.FirstName;
        user.LastName = string.IsNullOrEmpty(dto.LastName) ? user.LastName : dto.LastName;
        user.Address = string.IsNullOrEmpty(dto.Address) ? user.Address : dto.Address;
        user.Phone = dto.PhoneNumber > 0 ? dto.PhoneNumber : user.Phone;

        return user;
    }
}