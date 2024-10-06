using Grpc.Net.Client;

namespace WebHost.UserServiceClient;

public class UserServiceClient
{
    private readonly UserManager.UserManagerClient _client;

    public UserServiceClient(string address)
    {
        var channel = GrpcChannel.ForAddress(address);
        _client = new UserManager.UserManagerClient(channel);
    }

    public async Task<GetUserResponse> GetUserAsync(string userId)
    {
        try
        {
            var request = new GetUserRequest { Id = userId };
            return await _client.GetUserAsync(request);
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            throw;
        }
    }

    public async Task<CreateUserResponse> CreateUserAsync(UserInfo userInfo)
    {
        try
        {
            var request = new CreateUserRequest { User = userInfo };
            return await _client.CreateUserAsync(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            throw;
        }
    }

    public async Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest updateUserRequest)
    {
        try
        {
            return await _client.UpdateUserAsync(updateUserRequest);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            throw;
        }
    }

    public async Task<DeleteUserResponse> DeleteUserAsync(string userId, string login)
    {
        try
        {
            var request = new DeleteUserRequest();

            if (!string.IsNullOrEmpty(userId))
                request.Id = userId;
            else if (!string.IsNullOrEmpty(login))
                request.Login = login;
            else
                throw new ArgumentException();

            return await _client.DeleteUserAsync(request);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            throw;
        }
    }
}