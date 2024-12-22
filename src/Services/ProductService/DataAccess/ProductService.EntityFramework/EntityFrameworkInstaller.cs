using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProductService.DataAccess.EntityFramework
{
    public static class EntityFrameworkInstaller
    {
        public static IServiceCollection ConfigureContext(this IServiceCollection services, IConfiguration configuration)
        {
            const string host = "PRODUCT_POSTGRES_HOST";
            const string db = "PRODUCT_POSTGRES_DB";
            const string port = "PRODUCT_POSTGRES_PORT";
            const string user = "PRODUCT_POSTGRES_USER";
            const string password = "PRODUCT_POSTGRES_PASSWORD";

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
