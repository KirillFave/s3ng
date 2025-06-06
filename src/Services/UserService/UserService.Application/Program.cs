using Microsoft.AspNetCore.Server.Kestrel.Core;
using UserService.Application.Mapping;
using UserService.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Repository;
using UserService.Infrastructure.EFCore;
using Serilog;
using Microsoft.AspNetCore.Diagnostics;
using UserService.Application;
using UserService.KafkaConsumer.Consumers;
using UserService.KafkaConsumer.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddAutoMapper(typeof(UserServiceMappingProfile));

builder.Services.AddScoped<IUserRepository, UserRepository>();

var configuration = builder.Configuration;

builder.Services.AddDbContext<UserServiceContext>(optionsBuilder
    => optionsBuilder
        .UseLazyLoadingProxies()
        .UseNpgsql(configuration.GetConnectionString("UserDb")));

builder.WebHost.ConfigureKestrel(options =>
{   //TODO
    options.ListenAnyIP(5005, listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
});

builder.Host.UseSerilog(LoggerHelper.AddLogger(configuration));

var options = configuration.Get<ApplicationOptions>();
builder.Services.AddSingleton(options);

builder.Services.AddHostedService<UserRegistredConsumer>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<UserServiceContext>();

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
                logger.Error(exceptionHandlerFeature.Error, "UseExceptionHandler поймал ошибку в UserService");
            }
        }
        return Task.CompletedTask;
    });
});

app.MapGrpcService<UserManagerService>();

app.Run();
