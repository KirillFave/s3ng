using Microsoft.AspNetCore.Server.Kestrel.Core;
using ProductService.Api.Mappers;
using ProductService.Api.Settings;
using ProductService.Application;
using ProductService.Domain;
using ProductService.Infrastructure;
using Serilog;
using SharedLibrary.ProductService.Minio;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.Services.Configure<S3Options>(builder.Configuration.GetSection(S3Options.S3));

builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureS3(builder.Configuration);

builder.Services.AddSingleton<IProductRepository, MongoProductRepository>();
builder.Services.AddSingleton<IFileStorage, S3FileStorage>();
builder.Services.AddSingleton<IProductManagementService, ProductManagementService>();

builder.Services.AddAutoMapper(typeof(ProductMappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(o =>
{
    o.ListenAnyIP(50052, listenOptions => listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3);
});

builder.Host.UseSerilog(LoggerConfig.AddLogger(builder.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.Run();
