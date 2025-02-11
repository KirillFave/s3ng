using FluentValidation;
using s3ng.Contracts.IAM;
using WebHost.Dto.IAM;
using WebHost.Validators;

namespace WebHost.Configuration.IAMConfiguration
{
    public static class ConfigurationIAMServiceClient
    {
        public static IServiceCollection AddIAMServiceClient(this IServiceCollection services, IConfiguration configuration)
        {
            var iamServiceUri = configuration["IAM_SERVICE_URI"] ??
                    throw new Exception("IAM_SERVICE_URI is not specified in ENV");
            var uri = new Uri(iamServiceUri);

            services.AddScoped<IValidator<RegistrationRequestDto>, RegistrationRequestValidator>();
            services.AddGrpcClient<Registration.RegistrationClient>(x =>
            {
                x.Address = uri;
            });

            services.AddScoped<IValidator<AuthenticationRequestDto>, AuthenticationRequestValidator>();
            services.AddGrpcClient<Authentication.AuthenticationClient>(x =>
            {
                x.Address = uri;
            });

            return services;
        }
    }
}
