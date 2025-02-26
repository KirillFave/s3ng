using Confluent.Kafka;
using IAM.DAL;
using IAM.Infra;
using IAM.Mappers;
using IAM.Producers;
using IAM.Repositories;
using IAM.Seedwork;
using IAM.Seedwork.Abstractions;
using IAM.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SharedLibrary.Common.Kafka;
using SharedLibrary.IAM.JWT;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}
//Типа для будущей секурности. Если запускаемся под продакшен конфигурацией, то переменные будут подпихиваться из окружения
if (builder.Environment.IsProduction())
{
    builder.Configuration.AddEnvironmentVariables();
}


//Main
builder.Services.AddGrpc();
builder.WebHost.ConfigureListen(builder.Configuration);

//Configuration
builder.Services.ConfigureContext(builder.Configuration);
builder.Services.ConfigureDistributedCache(builder.Configuration);
builder.Host.UseSerilog(LoggerConfig.AddLogger(builder.Configuration));
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.Jwt));
builder.Services.Configure<KafkaOptions>(builder.Configuration.GetSection(KafkaOptions.Kafka));

//Mappers
builder.Services.AddAutoMapper(typeof(AccountProfile));

//Services
builder.Services.AddTransient<ITokenProvider, JwtTokenProvider>();
builder.Services.AddTransient(typeof(RefreshTokenRepository));
builder.Services.AddSingleton(typeof(UserRegistredProducer));
builder.Services.AddScoped(typeof(UserRepository));

var app = builder.Build();

//Migration
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

//Global exceptionHandler
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

//GRPC
app.MapGrpcService<RegistrationService>();
app.MapGrpcService<AuthenticationService>();

app.Run();
