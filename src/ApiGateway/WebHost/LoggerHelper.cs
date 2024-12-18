using Serilog.Events;
using Serilog;
using ILogger = Serilog.ILogger;
using System.Text.Json;

namespace WebHost;
public static class LoggerHelper
{
    public static ILogger AddLogger(IConfiguration configuration)
    {
        var lc = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Destructure.AsScalar<JsonDocument>()
            .WriteTo.Console()
            .WriteTo.Seq("http://seq:5341")
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing.EndpointMiddleware", LogEventLevel.Information)
            .MinimumLevel.Override("Grpc", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore.StaticFiles.StaticFileMiddleware", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore.Cors.Infrastructure.CorsService", LogEventLevel.Information)
            .Enrich.WithProperty("ServiceName", "WebHost");

        return lc.CreateLogger();
    }
}
