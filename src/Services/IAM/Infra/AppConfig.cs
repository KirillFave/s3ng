using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace IAM.Infra
{
    public static class AppConfig
    {
        public static void ConfigureListen(this IWebHostBuilder webHost, IConfiguration configuration)
        {
            const string servicePortSectionName = "AppSettings:ServicePort";
            var portValue = configuration.GetValue<int>(servicePortSectionName);

            webHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(portValue, listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
            });
        }
    }
}
