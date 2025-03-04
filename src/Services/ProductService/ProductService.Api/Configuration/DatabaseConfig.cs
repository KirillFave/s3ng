using MongoDB.Driver;

namespace ProductService.Api.Configuration
{
    public static class DatabaseConfig
    {
        public static void ConfigureDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb");

            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            serviceCollection.AddSingleton(sp =>
            {
                return new MongoClient(connectionString).GetDatabase("ProductDb");
            });
        }
    }
}
