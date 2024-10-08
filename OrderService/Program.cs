using OrderService.Database;
using OrderService.Models;
using OrderService.Repositories;

using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.Sources.Clear();
builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(
    optionsBuilder => optionsBuilder.UseSqlite(builder.Configuration["ConnectionString"]));

builder.Services.AddScoped(typeof(IRepository<Order>), typeof(EfRepository<Order>));
//builder.Services.AddScoped(typeof(IRepository<OrderItem>), (x) =>
//    new EfRepository<Customer>(FakeDataFactory.Customers));

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
    options.Title = "OrderService API Doc";
    options.Version = "1.0";
});

builder.Services.AddAutoMapper(typeof(Program));

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

app.Run();
