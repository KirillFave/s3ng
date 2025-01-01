using OrderService.Database;
using OrderService.Repositories;

using Microsoft.EntityFrameworkCore;

namespace WebHost.OrderServiceConfiguration;

public static class OrderServiceConfiguration
{
    public static IServiceCollection ConfigureOrderService(this IServiceCollection services, 
                                                           IConfiguration configuration)
    {
        services.AddHttpClient("OrderService", client =>
        {
            client.BaseAddress = new Uri(configuration["ORDER_SERVICE_URI"] ??
                   throw new Exception("ORDER_SERVICE_URI is not specified in ENV"));
        });

        return services;
    }
}
