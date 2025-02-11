using IAM.DAL;
using Microsoft.EntityFrameworkCore;

namespace IAM.Infra
{
    public static class DbConfig
    {
        public static IServiceCollection ConfigureContext(this IServiceCollection services, IConfiguration configuration)
        {
            const string connectionSectionName = "DefaultConnection";

            var connectionString = configuration.GetConnectionString(connectionSectionName)
                                   ?? throw new ArgumentNullException(connectionSectionName);

            services.AddDbContext<DatabaseContext>(optionsBuilder
                => optionsBuilder
                    .UseNpgsql(connectionString));

            return services;
        }
    }
}
