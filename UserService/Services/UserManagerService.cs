using Grpc.Core;

namespace UserService.Services;

public class UserManagerService : UserManager.UserManagerBase
{
    public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
    {
        return await base.CreateUser(request, context);
    }

    public override async Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        //todo
        return new GetUserResponse()
        {
            Result = GetUserResponse.Types.GetUserResult.GetUserByIdResultSuccess,
            User = new UserInfo
            {
                FirstName = "Vlad",
                Phone = 789668555,
                LastName = "Ivanov",
                Role = Role.UserRoleBuyer,
                Address = "Novosibirsk"
            }
        };
    }

    public override async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
        return await base.UpdateUser(request, context);
    }

    public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        return await base.DeleteUser(request, context);
    }
}