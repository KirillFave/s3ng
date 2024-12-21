using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;
using WebHost;
using WebHost.Controllers;
using WebHost.IAMConfiguration;
using WebHost.Mappers;
using WebHost.ProductServiceClient;
using WebHost.UserServiceClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//automapper
builder.Services.AddAutoMapper(typeof(IAMProfile));
builder.Services.AddAutoMapper(typeof(UsersMappingProfile));

//general
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpc();

var configuration = builder.Configuration;

//Авторизация аутентификация
builder.Services.AddJwtAuthentication(configuration);

//Сервисы
builder.Services.AddIAMServiceClient(configuration);
builder.Services.AddUserServiceClient(configuration);
builder.Services.AddProductServiceClient(configuration);

builder.WebHost.ConfigureKestrel(o =>
{
    o.ListenAnyIP(8080, listenOptions => listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3);
});

builder.Host.UseSerilog(LoggerHelper.AddLogger(configuration));

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(context =>
    {
        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (exceptionHandlerFeature != null)
        {
            if (app.Services.GetService<Serilog.ILogger>() is { } logger)
            {
                logger.Error(exceptionHandlerFeature.Error, "UseExceptionHandler поймал ошибку в WebHost");
            }
        }
        return Task.CompletedTask;
    });
});

app.Run();
