using Serilog;
using ILogger = Serilog.ILogger;
using System.Text.Json;

namespace OrderService.Configuration;

public static class LoggerConfig
{
    public static ILogger AddLogger()
    {
        var lc = new LoggerConfiguration()
           .MinimumLevel.Verbose()
           .Destructure.AsScalar<JsonDocument>()
           .WriteTo.Console()
           .WriteTo.Seq("http://seq:5341")
           .Enrich.WithProperty("ServiceName", "OrderService");

        return lc.CreateLogger();
    }
}
