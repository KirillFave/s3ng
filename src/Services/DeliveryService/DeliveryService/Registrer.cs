using DeliveryService.Delivery.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DeliveryService
{
    public static class Registrer
    {
        public static IServiceCollection AddApplicationDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            //var connections = configuration.GetConnectionString("Postgres");
            var connections = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DeliveryDBContext>(options =>
            {
                options.UseNpgsql(connections,
                optionsBuilder => optionsBuilder.MigrationsAssembly("DeliveryService"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            return services;
        }
    }
}
