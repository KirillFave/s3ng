using Microsoft.EntityFrameworkCore;

namespace UserService.EFCore;

public static class Configuration
{
    public static IServiceCollection ConfigureContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = $"Host={configuration["USER_SERVICE_POSTGRES_HOST"] ?? "localhost"};" +
                               $"Port={configuration["USER_SERVICE_POSTGRES_PORT"] ?? "5432"};" +
                               $"Database={configuration["USER_SERVICE_POSTGRES_DB"] ?? "UsersBD"};" +
                               $"Username={configuration["USER_SERVICE_POSTGRES_USER"] ?? "userservise"};" +
                               $"Password={configuration["USER_SERVICE_POSTGRES_PASSWORD"] ?? "123qwe"}";

        var ff = configuration["USER_SERVICE_URI"];

        services.AddDbContext<UserServiceContext>(optionsBuilder
            => optionsBuilder
                .UseLazyLoadingProxies()
                .UseNpgsql(connectionString));

        return services;
    }
}