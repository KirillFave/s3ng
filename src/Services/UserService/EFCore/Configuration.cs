using Microsoft.EntityFrameworkCore;

namespace UserService.EFCore;

public static class Configuration
{
    public static IServiceCollection ConfigureContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = $"Host={configuration["USER_SERVICE_POSTGRES_HOST"]};" +
                               $"Port={configuration["USER_SERVICE_POSTGRES_PORT"]};" +
                               $"Database={configuration["USER_SERVICE_POSTGRES_DB"]};" +
                               $"Username={configuration["USER_SERVICE_POSTGRES_USER"]};" +
                               $"Password={configuration["USER_SERVICE_POSTGRES_PASSWORD"]}";

        services.AddDbContext<UserServiceContext>(optionsBuilder
            => optionsBuilder
                .UseLazyLoadingProxies()
                .UseNpgsql(connectionString));

        return services;
    }
}