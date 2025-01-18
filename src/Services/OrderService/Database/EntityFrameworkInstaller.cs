using Microsoft.EntityFrameworkCore;

namespace OrderService.Database;

public static class EntityFrameworkInstaller
{
    public static IServiceCollection ConfigureContext(this IServiceCollection services, IConfiguration configuration)
    {
        const string host = "ORDER_POSTGRES_HOST";
        const string db = "ORDER_POSTGRES_DB";
        const string port = "ORDER_POSTGRES_PORT";
        const string user = "ORDER_POSTGRES_USER";
        const string password = "ORDER_POSTGRES_PASSWORD";

        var connectionString = $"Host={configuration[host] ?? throw new ArgumentNullException(host)};" +
                               $"Port={configuration[port] ?? throw new ArgumentNullException(port)};" +
                               $"Database={configuration[db] ?? throw new ArgumentNullException(db)};" +
                               $"Username={configuration[user] ?? throw new ArgumentNullException(user)};" +
                               $"Password={configuration[password] ?? throw new ArgumentNullException(password)}";

        services.AddDbContext<DatabaseContext>(optionsBuilder
            => optionsBuilder
                .UseNpgsql(connectionString));

        return services;
    }
}
