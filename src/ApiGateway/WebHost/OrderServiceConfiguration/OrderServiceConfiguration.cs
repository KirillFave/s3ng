using OrderService.Database;
using OrderService.Repositories;

using Microsoft.EntityFrameworkCore;

namespace WebHost.OrderServiceConfiguration;

public static class OrderServiceConfiguration
{
    public static IServiceCollection ConfigureOrderService(this IServiceCollection services, 
                                                           IConfiguration configuration)
    {
        services.AddScoped(typeof(OrderRepository));
        services.AddScoped(typeof(OrderItemRepository));

        services.AddDbContext<DatabaseContext>(
            optionsBuilder => optionsBuilder.UseSqlite(configuration["OrderService.ConnectionString"]));

        return services;
    }
}
