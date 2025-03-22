using CartService.Services;

namespace CartService.Extensions;

public static class RedisExtension
{
    public static IServiceCollection AddRedisCache(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddScoped<ICartCacheService, CartCacheService>();

        const string connectionSectionName = "Redis";

        var connectionString = configuration.GetConnectionString(connectionSectionName)
                               ?? throw new ArgumentNullException(connectionSectionName);

        serviceCollection.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = connectionString;
        });

        return serviceCollection;
    }
}
