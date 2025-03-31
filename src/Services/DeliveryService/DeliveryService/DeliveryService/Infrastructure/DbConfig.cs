using System;
using DeliveryService.Delivery.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace DeliveryService.Infrastructure
{
    public static class DbConfig
 {
    public static IServiceCollection ConfigureContext(this IServiceCollection services, IConfiguration configuration)
    {
        const string connectionSectionName = "DefaultConnection";

        var connectionString = configuration.GetConnectionString(connectionSectionName)
                               ?? throw new ArgumentNullException(connectionSectionName);

        services.AddDbContext<DeliveryDBContext>(optionsBuilder
            => optionsBuilder
                .UseNpgsql(connectionString));

        return services;
    }

    public static IServiceCollection ConfigureDistributedCache(this IServiceCollection services, IConfiguration configuration)
    {
        const string connectionSectionName = "Redis";

            var connectionString = configuration.GetConnectionString(connectionSectionName);
                               //?? throw new ArgumentNullException(connectionSectionName);

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            return ConnectionMultiplexer.Connect(connectionString);
        });
        return services;
    }
  }
}
