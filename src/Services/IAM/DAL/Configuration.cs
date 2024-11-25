using Microsoft.EntityFrameworkCore;

namespace IAM.DAL
{
    public static class Configuration
    {
        public static IServiceCollection ConfigureContext(this IServiceCollection services, IConfiguration configuration)
        {
            const string host = "IAM_SERVICE_POSTGRES_HOST";
            const string db = "IAM_SERVICE_POSTGRES_DB";
            const string port = "IAM_SERVICE_POSTGRES_PORT";
            const string user = "IAM_SERVICE_POSTGRES_USER";
            const string password = "IAM_SERVICE_POSTGRES_PASSWORD";

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
}
