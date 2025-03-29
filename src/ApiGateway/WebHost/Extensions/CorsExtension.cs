namespace WebHost.Extensions;

public static class CorsExtension
{
    public static IServiceCollection AddCorsWithFrontendPolicy(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(options =>
        {
            options.AddPolicy(name: "Frontend",
                policy =>
                {
                    policy.WithOrigins("http://client:3000") //TODO вынести в env
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });

        return serviceCollection;
    }
}
