using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserService.Application.Test;

public class UserTestFixture : IDisposable
{
    public IServiceProvider ServiceProvider { get; set; }

    /// <summary>
    /// Выполняется перед запуском тестов
    /// </summary>
    public UserTestFixture()
    {
        var builder = new ConfigurationBuilder();
        var configuration = builder.Build();
        var serviceCollection = TestConfiguration.GetServiceCollection(configuration);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        ServiceProvider = serviceProvider;
    }

    public void Dispose() { }
}
