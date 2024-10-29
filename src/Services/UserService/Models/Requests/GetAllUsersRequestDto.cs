using MediatR;
using UserService.Models.Response;

namespace UserService.Models.Requests;

public class GetAllUsersRequestDto : IRequest<GetAllUsersResponseDto>
{
}