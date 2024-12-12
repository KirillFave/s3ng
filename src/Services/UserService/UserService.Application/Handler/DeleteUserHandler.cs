using MediatR;
using UserService.Infrastructure.Repository;
using UserService.Application.Models.Requests;
using UserService.Application.Models.Response;
using UserService.Application.Models.Results;
using ILogger = Serilog.ILogger;

namespace UserService.Application.Handler;

public class DeleteUserHandler : IRequestHandler<DeleteUserRequestDto, DeleteUserResponseDto>
{
    private readonly IUserRepository _repository;
    private readonly ILogger _logger;

    public DeleteUserHandler(IUserRepository repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<DeleteUserResponseDto> Handle(DeleteUserRequestDto request, CancellationToken cancellationToken)
    {
        _logger.Information("Пришёл запрос на удаление user с Id = {Id}", request.Id);

        var deleteUserResponseDto = new DeleteUserResponseDto();
        try
        {
            _logger.Information("Приступаю к исполнению запроса DeleteUserRequest");
            var isSuccess = await _repository.DeleteAsync(request.Id, cancellationToken);

            if (!isSuccess)
            {
                _logger.Error("Не смогли отработать запрос DeleteUserRequest, результат удаления user из BD равен false");
                deleteUserResponseDto.Result = DeleteUserResultModel.Fail;
                return deleteUserResponseDto;
            }

            await _repository.SaveChangesAsync(cancellationToken);
            _logger.Information("Успешно отработали запрос DeleteUserRequest");

            deleteUserResponseDto.Result = DeleteUserResultModel.Success;
            return deleteUserResponseDto;
        }
        catch(Exception e)
        {
            _logger.Error(e, "Исключение при попытке отработать запрос DeleteUserRequest");
            deleteUserResponseDto.Result = DeleteUserResultModel.Fail;
            return deleteUserResponseDto;
        }
    }
}
