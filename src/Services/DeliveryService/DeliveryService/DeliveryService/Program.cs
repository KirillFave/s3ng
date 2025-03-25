using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Abstractions;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Repositories;
using DeliveryService.Delivery.BusinessLogic.Services.DeliveryService;
using DeliveryService.Delivery.DataAccess.Data;
using System.Text.Json.Serialization;
using System.Text.Json;
using DeliveryService;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//builder.Services.AddDbContext<OrderContext>(opt =>
//        opt.UseNpgsql(builder.Configuration.GetConnectionString("OrderDefaultConnection")),
//    ServiceLifetime.Singleton
//);

//builder.Services.AddSingleton<IConsumer<Null, string>>(sp =>
//{
//    var config = new ConsumerConfig
//    {
//        GroupId = "DeliveryService",
//        BootstrapServers = "localhost:9092",
//        AutoOffsetReset = AutoOffsetReset.Earliest
//    };
//    return new ConsumerBuilder<Null, string>(config).Build();
//});

builder.Services.AddDbContext<DeliveryDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IDeliveryService, DeliveryServices>();
//builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
//builder.Services.AddHostedService<OrderConsumerService>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

//app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase<DeliveryDBContext>();


app.Run();
