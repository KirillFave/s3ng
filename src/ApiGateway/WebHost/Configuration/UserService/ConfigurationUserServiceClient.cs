namespace WebHost.Configuration.UserServiceClient;

public static class ConfigurationUserServiceClient
{
    public static IServiceCollection AddUserServiceClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcClient<UserManager.UserManagerClient>(
           o =>
           {
               o.Address = new Uri(configuration["USER_SERVICE_URI"] ??
                   throw new Exception("USER_SERVICE_URI is not specified in ENV"));
           });

        return services;
    }
}
