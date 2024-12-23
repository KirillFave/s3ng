using AutoMapper;
using MediatR;
using UserService.Infrastructure.Repository;
using UserService.Application.Models;
using UserService.Application.Models.Requests;
using UserService.Application.Models.Response;
using UserService.Application.Models.Results;
using ILogger = Serilog.ILogger;

namespace UserService.Application.Handler;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequestDto, GetUserResponseDto>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetUserByIdHandler(IUserRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetUserResponseDto> Handle(GetUserByIdRequestDto request, CancellationToken cancellationToken)
    {
        _logger.Information("Пришёл запрос на получение информации о user с Id = {Id}", request.Id);

        var getUserResponseDto = new GetUserResponseDto();
        try
        {
            _logger.Information("Приступаю к исполнению запроса GetUserByIdRequest");
            var user = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                _logger.Error("Не смогли отработать запрос GetUserByIdRequest, т.к. user с Id = {Id} не найден в BD", request.Id);
                getUserResponseDto.Result = GetUserResultModel.NotFound;
                return getUserResponseDto;
            }

            _logger.Information("Успешно отработали запрос GetUserByIdRequest");

            var userDto = _mapper.Map<UserDto>(user);
            getUserResponseDto.Result = GetUserResultModel.Success;
            getUserResponseDto.User = userDto;
            return getUserResponseDto;
        }
        catch (Exception e)
        {
            _logger.Error(e, "Исключение при попытке отработать запрос GetUserByIdRequest");
            getUserResponseDto.Result = GetUserResultModel.Fail;
            return getUserResponseDto;
        }
    }
}
