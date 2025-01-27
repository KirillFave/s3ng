using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Models;
using Serilog;
using WebHost;
using WebHost.Controllers;
using WebHost.OrderServiceConfiguration;
using WebHost.IAMConfiguration;
using WebHost.Mappers;
using WebHost.ProductServiceClient;
using WebHost.UserServiceClient;

var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.Sources.Clear();
//builder.Configuration.AddJsonFile("appsettings.json");
// Add services to the container.
//automapper
builder.Services.AddAutoMapper(typeof(IAMProfile));
builder.Services.AddAutoMapper(typeof(UsersMappingProfile));

//general
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddGrpc();

var configuration = builder.Configuration;

//Авторизация аутентификация
builder.Services.AddJwtAuthentication(configuration);

//Сервисы
builder.Services.AddIAMServiceClient(configuration);
builder.Services.AddUserServiceClient(configuration);
builder.Services.AddProductServiceClient(configuration);
builder.Services.ConfigureOrderService(configuration);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "s3ng API", Version = "v1" });
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Введите JWT токен",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = JwtConstants.HeaderType,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.WebHost.ConfigureKestrel(o =>
{
    o.ListenAnyIP(8080, listenOptions => listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3);
});

builder.Host.UseSerilog(LoggerHelper.AddLogger(configuration));

builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

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
