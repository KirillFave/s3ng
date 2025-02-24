using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Abstractions;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Repositories;
using DeliveryService.Delivery.BusinessLogic.Services.DeliveryService;
using DeliveryService.Delivery.DataAccess.Data;
using System.Text.Json.Serialization;
using System.Text.Json;
using DeliveryService;

var builder = WebApplication.CreateBuilder(args);

//builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//builder.Services.AddControllersWithViews();

builder.Services.AddApplicationDataContext(builder.Configuration);

builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddTransient<IDeliveryService, DeliveryServices>();


//var apiDeliveryService = builder.AddProject<Projects.DeliveryService>("apiservice-delivery");

//var apiOrderService = builder.AddProject<Projects.OrderService>("apiservice-order");

//builder.AddProject<Projects.ApiGateway("webfrontend")
//    .WithReference(apiDeliveryService)
//    .WithReference(apiOrderService);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase<DeliveryDBContext>();


app.Run();
