using Refit;
using SharedLibrary.ProductService.Contracts;

namespace WebHost.Configuration.ProductServiceClient;

public static class ConfigurationProductClient
{
    public static IServiceCollection AddProductServiceClient(this IServiceCollection services, IConfiguration configuration)
    {
        var productServiceUri = configuration["PRODUCT_SERVICE_URI"] ??
                throw new Exception("PRODUCT_SERVICE_URI is not specified in ENV");
        services.AddRefitClient<IProductServiceClient>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(productServiceUri));

        return services;
    }
}
