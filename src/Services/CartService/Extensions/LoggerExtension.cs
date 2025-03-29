using System.Text.Json;
using Serilog;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace CartService.Extensions;

public static class LoggerExtension
{
    public static ILogger AddLogger()
    {
        var lc = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Destructure.AsScalar<JsonDocument>()
            .WriteTo.Console()
            .WriteTo.Seq("http://seq:5341")
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing.EndpointMiddleware", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.AspNetCore.StaticFiles.StaticFileMiddleware", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft.AspNetCore.Cors.Infrastructure.CorsService", LogEventLevel.Error)
            .Enrich.WithProperty("ServiceName", "CartService");

        return lc.CreateLogger();
    }
}
