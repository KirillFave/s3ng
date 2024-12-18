using s3ng.ProductService.DataAccess.Repositories.Implementations;
using s3ng.ProductService.DataAccess.EntityFramework;
using s3ng.ProductService.Services.Abstractions;
using s3ng.ProductService.Services.Repositories.Abstractions;
using s3ng.ProductService.Host.Settings;
using s3ng.ProductService.Host;
using AutoMapper;
using Serilog;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var applicationSettings = builder.Configuration.Get<ApplicationSettings>();

        // Add AutoMappers to the container
        builder.Services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));

        // Add DbContexts to the container
        builder.Services.ConfigureContext(applicationSettings.ConnectionString);

        // Add services to the container
        builder.Services.AddSingleton(applicationSettings);
        builder.Services.AddSingleton((IConfigurationRoot)builder.Configuration);

        builder.Services.AddTransient<IProductService, s3ng.ProductService.Services.ProductService>();
        builder.Services.AddTransient<IProductRepository, ProductRepository>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Host.UseSerilog(LoggerHelper.AddLogger(builder.Configuration));

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
    }

    private static MapperConfiguration GetMapperConfiguration()
    {
        var configuration = new MapperConfiguration(config =>
        {
            config.AddProfile<s3ng.ProductService.Host.Mapping.ProductMappingsProfile>();
            config.AddProfile<s3ng.ProductService.ProductService.Mapping.ProductMappingsProfile>();
        });

        configuration.AssertConfigurationIsValid();

        return configuration;
    }
}
