using OrderService.Configuration;
using OrderService.Database;
using OrderService.Mapping;
using OrderService.Producers;
using OrderService.Repositories;

using DotNetEnv;
using DotNetEnv.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;
using SharedLibrary.Common.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddDotNetEnvMulti([".env"], LoadOptions.DEFAULT);

builder.Services.ConfigureContext(builder.Configuration);

builder.Services.AddControllers();

// Repositories
builder.Services.AddScoped(typeof(OrderRepository));
builder.Services.AddScoped(typeof(OrderItemRepository));

builder.Services.AddOpenApiDocument(options =>
{
    options.PostProcess = document =>
    {
        foreach (var schema in document.Definitions.Values)
        {
            foreach (var property in schema.Properties.Values)
            {
                if (property.Type == NJsonSchema.JsonObjectType.String && property.Format == "guid")
                {
                    property.Format = "uuid";
                }
            }
        }
    };
    options.Title = "API Doc";
    options.Version = "1.0";
});

// Kafka
builder.Services.Configure<KafkaOptions>(builder.Configuration.GetSection(KafkaOptions.Kafka));

// MappingProfile
builder.Services.AddAutoMapper(typeof(OrderMappingProfile));
builder.Services.AddAutoMapper(typeof(OrderItemMappingProfile));

// Producers
builder.Services.AddSingleton(typeof(OrderCreatedProducer));
builder.Services.AddSingleton(typeof(OrderCanceledProducer));

builder.Host.UseSerilog(LoggerConfig.AddLogger());

builder.Services.AddEndpointsApiExplorer();

builder.WebHost.ConfigureKestrel(o =>
{
    o.ListenAnyIP(50055, listenOptions => listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3);
});

var app = builder.Build();
    
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseOpenApi();
app.UseSwaggerUi(x =>
{
    x.DocExpansion = "list";
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.UseAuthorization();

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
