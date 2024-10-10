using s3ng.Contracts.IAM;

namespace s3ng.WebHost.RegisterServiceClient
{
    public static class ConfigurationRegistrationServiceClient
    {
        public static IServiceCollection AddRegistrationServiceClient(this IServiceCollection services, IConfiguration configuration)
        {
            var iamServiceUri = configuration["IAM_SERVICE_URI"];
            services.AddGrpcClient<Registration.RegistrationClient>(x =>
            {
                x.Address = new Uri(iamServiceUri ??
                    throw new Exception("IAM_SERVICE_URI is not specified in ENV"));
            });

            return services;
        }
    }
}
