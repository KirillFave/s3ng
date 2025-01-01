using Serilog.Events;
using Serilog;
using ILogger = Serilog.ILogger;
using System.Text.Json;

namespace ProductService.Host;

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
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing.EndpointMiddleware", LogEventLevel.Error)
            .MinimumLevel.Override("Grpc", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore.StaticFiles.StaticFileMiddleware", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.AspNetCore.Cors.Infrastructure.CorsService", LogEventLevel.Error)
            .Enrich.WithProperty("ServiceName", "ProductService");

        return lc.CreateLogger();
    }
}
