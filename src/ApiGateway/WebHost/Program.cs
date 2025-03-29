using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Models;
using Serilog;
using WebHost.Configuration;
using WebHost.Configuration.IAMConfiguration;
using WebHost.Configuration.OrderServiceConfiguration;
using WebHost.Configuration.ProductServiceClient;
using WebHost.Configuration.UserServiceClient;
using WebHost.Extensions;
using WebHost.Mappers;

var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.Sources.Clear();
//builder.Configuration.AddJsonFile("appsettings.json");
// Add services to the container.
//automapper
builder.Services.AddAutoMapper(typeof(IAMProfile));
builder.Services.AddAutoMapper(typeof(OrderMappingProfile));
builder.Services.AddAutoMapper(typeof(OrderItemMappingProfile));
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
        BearerFormat = JwtBearerDefaults.AuthenticationScheme,
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

builder.Host.UseSerilog(ConfigLogger.AddLogger(configuration));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCorsWithFrontendPolicy();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger(c =>
{
    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
    {
        swaggerDoc.Servers = new List<OpenApiServer>
        {
            new OpenApiServer { Url = "http://localhost/api" }
        };
    });
});

app.UseSwaggerUI();

app.UseCors("Frontend");

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
