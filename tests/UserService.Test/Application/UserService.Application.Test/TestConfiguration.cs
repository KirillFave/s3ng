using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using UserService.Application.Mapping;
using UserService.Infrastructure.Repository;

namespace UserService.Application.Test
{
    public static class TestConfiguration
    {
        public static IServiceCollection GetServiceCollection(IConfigurationRoot configuration, IServiceCollection serviceCollection = null)
        {
            if (serviceCollection == null)
            {
                serviceCollection = new ServiceCollection();
            }

            serviceCollection
                .AddSingleton(configuration)
                .AddSingleton((IConfiguration)configuration)
                .AddMediatR(typeof(Program))
                .AddAutoMapper(typeof(UserServiceMappingProfile))
                .AddScoped<IUserRepository, UserRepository>();

            return serviceCollection;
        }
    }
}
