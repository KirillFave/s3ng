using DotNetEnv;
using DotNetEnv.Configuration;
using IAM;
using IAM.DAL;
using IAM.Mappers;
using IAM.Seedwork;
using IAM.Seedwork.Abstractions;
using IAM.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SharedLibrary.Common.Kafka;
using SharedLibrary.IAM.JWT;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddTransient<ITokenProvider, JwtTokenProvider>();

builder.Services.AddAutoMapper(typeof(AccountProfile));

builder.Configuration.AddDotNetEnvMulti([".env" ], LoadOptions.TraversePath());
builder.Services.ConfigureContext(builder.Configuration);
builder.WebHost.ConfigureListen(builder.Configuration);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.Jwt));
builder.Services.Configure<KafkaOptions>(builder.Configuration.GetSection(KafkaOptions.Kafka));

builder.Host.UseSerilog(LoggerHelper.AddLogger(builder.Configuration));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DatabaseContext>();

    try
    {
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка применения миграций: {ex.Message}");
    }
}

app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(context =>
    {
        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (exceptionHandlerFeature != null)
        {
            if (app.Services.GetService<Serilog.ILogger>() is { } logger)
            {
                logger.Error(exceptionHandlerFeature.Error, "UseExceptionHandler поймал ошибку в IAM");
            }
        }
        return Task.CompletedTask;
    });
});

app.MapGrpcService<RegistrationService>();
app.MapGrpcService<AuthenticationService>();

app.Run();
