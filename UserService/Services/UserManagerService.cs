using Grpc.Core;

namespace UserService.Services
{
    public class UserManagerService : UserManager.UserManagerBase
    {
        public UserManagerService()
        {

        }

        public override async Task<UserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            return await base.CreateUser(request, context);
        }

        public override async Task<UserResponse> GetUserById(GetUserByIdRequest request, ServerCallContext context)
        {
            return await base.GetUserById(request, context);
        }

        public override async Task<UserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
        {
            return await base.UpdateUser(request, context);
        }

        public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
        {
            return await base.DeleteUser(request, context);
        }
    }
}