using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Abstractions;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Repositories;
using DeliveryService.Delivery.BusinessLogic.Services.DeliveryService;
using DeliveryService.Delivery.DataAccess.Data;
using System.Text.Json.Serialization;
using System.Text.Json;
using DeliveryService;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Infrastructure;
using DeliveryService.Kafka.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<DeliveryDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IDeliveryService, DeliveryServices>();
//builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
//builder.Services.AddHostedService<OrderConsumerService>();

builder.Services.AddDistributedMemoryCache();
builder.Services.ConfigureDistributedCache(builder.Configuration);
builder.Services.AddSingleton(typeof(ConsumerBackgroundService));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Migration
app.MigrateDatabase<DeliveryDBContext>();

app.Run();
