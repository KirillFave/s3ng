using Microsoft.EntityFrameworkCore;

namespace UserService.EFCore;

public static class Configuration
{
    public static IServiceCollection ConfigureContext(this IServiceCollection services)
    {
        services.AddDbContext<UserServiceContext>(optionsBuilder
            => optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlite("Data Source = Users.db"));

        return services;
    }
}