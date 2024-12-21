using ProductService.DataAccess.Repositories.Implementations;
using ProductService.DataAccess.EntityFramework;
using ProductService.Services.Abstractions;
using ProductService.Services.Repositories.Abstractions;
using ProductService.Host.Settings;
using ProductService.Host;
using AutoMapper;
using Serilog;
using DotNetEnv;
using DotNetEnv.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using ProductService.ProductService.Mapping;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var applicationSettings = builder.Configuration.Get<ApplicationSettings>();

        builder.Configuration.AddDotNetEnvMulti([".env"], LoadOptions.TraversePath());

        builder.Services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));

        builder.Services.ConfigureContext(builder.Configuration);

        builder.Services.AddSingleton(applicationSettings);
        builder.Services.AddSingleton((IConfigurationRoot)builder.Configuration);

        builder.Services.AddTransient<IProductService, ProductService.Services.ProductService>();
        builder.Services.AddTransient<IProductRepository, ProductRepository>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Host.UseSerilog(LoggerHelper.AddLogger(builder.Configuration));

        builder.WebHost.ConfigureKestrel(o =>
        {
            o.ListenAnyIP(50052, listenOptions => listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

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

        app.Run();
    }

    private static MapperConfiguration GetMapperConfiguration()
    {
        var configuration = new MapperConfiguration(config =>
        {
            config.AddProfile<ProductMappingsProfile>();
        });

        configuration.AssertConfigurationIsValid();

        return configuration;
    }
}
