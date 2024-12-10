using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace s3ng.ProductService.DataAccess.EntityFramework
{
    public static class EntityFrameworkInstaller
    {
        public static IServiceCollection ConfigureContext(this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<DatabaseContext>(optionsBuilder
                => optionsBuilder
                    //.UseLazyLoadingProxies()
                    .UseSqlite(connectionString));
                    return services;
        }
    }
}