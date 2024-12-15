using s3ng.Contracts.IAM;

namespace WebHost.RegisterServiceClient
{
    public static class ConfigurationIAMServiceClient
    {
        public static IServiceCollection AddRegistrationServiceClient(this IServiceCollection services, IConfiguration configuration)
        {
            var iamServiceUri = configuration["IAM_SERVICE_URI"] ??
                    throw new Exception("IAM_SERVICE_URI is not specified in ENV");
            var uri = new Uri(iamServiceUri);

            services.AddGrpcClient<Registration.RegistrationClient>(x =>
            {
                x.Address = uri;
            });

            services.AddGrpcClient<Authentication.AuthenticationClient>(x =>
            {
                x.Address = uri;
            });

            return services;
        }
    }
}
