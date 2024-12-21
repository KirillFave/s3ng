namespace WebHost.ProductServiceClient;

public static class ConfigurationProductClient
{
    public static IServiceCollection AddProductServiceClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("ProductService", client =>
        {
            client.BaseAddress = new Uri(configuration["PRODUCT_SERVICE_URI"] ??
                   throw new Exception("PRODUCT_SERVICE_URI is not specified in ENV"));
        });

        return services;
    }
}
