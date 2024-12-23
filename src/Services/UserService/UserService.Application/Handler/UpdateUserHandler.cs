using MediatR;
using AutoMapper;
using UserService.Domain.Entities;
using UserService.Infrastructure.Repository;
using UserService.Application.Models;
using UserService.Application.Models.Requests;
using UserService.Application.Models.Response;
using UserService.Application.Models.Results;
using ILogger = Serilog.ILogger;

namespace UserService.Application.Handler;

public class UpdateUserHandler : IRequestHandler<UpdateUserRequestDto, UpdateUserResponseDto>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public UpdateUserHandler(IUserRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateUserResponseDto> Handle(UpdateUserRequestDto request, CancellationToken cancellationToken)
    {
        _logger.Information("Пришёл запрос на обновлении информации о user с Id = {Id}", request.Id);

        var updateUserResponseDto = new UpdateUserResponseDto();
        try
        {
            _logger.Information("Приступаю к исполнению запроса UpdateUserRequest");
            var user = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                _logger.Error("Не смогли отработать запрос UpdateUserRequest, т.к. user с Id = {Id} не найден в BD", request.Id);
                updateUserResponseDto.Result = UpdateUserResultModel.NotFound;
                return updateUserResponseDto;
            }

            var updateUser = await _repository.UpdateAsync(GetUpdateUser(request, user), cancellationToken);

            if (updateUser == null)
            {
                _logger.Error("Не смогли отработать запрос UpdateUserRequest, результат обновлении user в BD равен null");
                updateUserResponseDto.Result = UpdateUserResultModel.Fail;
                return updateUserResponseDto;
            }

            await _repository.SaveChangesAsync(cancellationToken);
            _logger.Information("Успешно отработали запрос UpdateUserRequest");

            var userDto = _mapper.Map<UserDto>(updateUser);
            updateUserResponseDto.User = userDto;
            updateUserResponseDto.Result = UpdateUserResultModel.Success;
            return updateUserResponseDto;
        }
        catch (Exception e)
        {
            _logger.Error(e, "Исключение при попытке отработать запрос UpdateUserRequest");
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
