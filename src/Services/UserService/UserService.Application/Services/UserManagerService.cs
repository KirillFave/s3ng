using AutoMapper;
using Grpc.Core;
using MediatR;
using UserService.Application.Models.Requests;

namespace UserService.Application.Services;

public class UserManagerService : UserManager.UserManagerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserManagerService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public override async Task<GetUserResponse> GetUserById(GetUserByIdRequest request, ServerCallContext context)
    {
        var getUserByIdReq = _mapper.Map<GetUserByIdRequestDto>(request);
        var getUserRsp = await _mediator.Send(getUserByIdReq);
        return _mapper.Map<GetUserResponse>(getUserRsp);
    }

    public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        var createUserReq = _mapper.Map<CreateUserRequestDto>(request);
        var createUserRsp = await _mediator.Send(createUserReq);
        return _mapper.Map<CreateUserResponse>(createUserRsp);
    }

    public override async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
        var updateUserReq = _mapper.Map<UpdateUserRequestDto>(request);
        var updateUserRsp = await _mediator.Send(updateUserReq);
        return _mapper.Map<UpdateUserResponse>(updateUserRsp);
    }

    public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        var deleteUserReq = _mapper.Map<DeleteUserRequestDto>(request);
        var deleteUserRsp = await _mediator.Send(deleteUserReq);
        return _mapper.Map<DeleteUserResponse>(deleteUserRsp);
    }
}