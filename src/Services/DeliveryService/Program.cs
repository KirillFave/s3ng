using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Text.Json;
using DeliveryService.Data;
using DeliveryService.Models;
using DeliveryService.Services.Services.Repositories;
using DeliveryService.Services.Services.Abstractions;
using DeliveryService.Services.DeliveryService;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();

//builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DeliveryDBContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddTransient<IDeliveryService, DeliveryServices>();


//var apiDeliveryService = builder.AddProject<Projects.DeliveryService>("apiservice-delivery");

//var apiOrderService = builder.AddProject<Projects.OrderSErvice>("apiservice-order");

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

     // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
       {
          app.UseSwagger();
          app.UseSwaggerUI();
       }

          app.UseHttpsRedirection();

          app.UseAuthorization();

          app.MapControllers();

          app.Run();
