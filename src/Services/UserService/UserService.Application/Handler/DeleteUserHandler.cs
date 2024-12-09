using MediatR;
using UserService.Infrastructure.Repository;
using UserService.Models.Requests;
using UserService.Models.Response;
using UserService.Models.Results;

namespace UserService.Handler;

public class DeleteUserHandler : IRequestHandler<DeleteUserRequestDto, DeleteUserResponseDto>
{
    private readonly IUserRepository _repository;
    public DeleteUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteUserResponseDto> Handle(DeleteUserRequestDto request, CancellationToken cancellationToken)
    {
        var deleteUserResponseDto = new DeleteUserResponseDto();
        try
        {
            var isSuccess = await _repository.DeleteAsync(request.Id, cancellationToken);

            if (!isSuccess)
            {
                deleteUserResponseDto.Result = DeleteUserResultModel.Fail;
                return deleteUserResponseDto;
            }

            await _repository.SaveChangesAsync(cancellationToken);

            deleteUserResponseDto.Result = DeleteUserResultModel.Success;
            return deleteUserResponseDto;
        }
        catch
        {
            deleteUserResponseDto.Result = DeleteUserResultModel.Fail;
            return deleteUserResponseDto;
        }
    }
}
