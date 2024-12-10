using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace IAM.Services
{
    public static class Configuration
    {
        public static void ConfigureListen(this IWebHostBuilder webHost, IConfiguration configuration)
        {
            const string portName = "IAM_SERVICE_PORT";
            var portValue = Convert.ToInt32(configuration[portName] ?? throw new ArgumentNullException(portName));

            webHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(portValue, listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
            });
        }
    }
}
