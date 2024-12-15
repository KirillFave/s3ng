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
;

builder.Host.UseSerilog(LogerHelper.AddLogger());

var app = builder.Build();

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
