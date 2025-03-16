using CartService.Extensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(o =>
{
    o.ListenAnyIP(5015, listenOptions => listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3);
});

builder.Host.UseSerilog(LoggerExtension.AddLogger());

builder.Services.AddSignalR();
builder.Services.AddRedisCache(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Frontend",
    policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Frontend");
app.UseRouting();

app.MapHub<CartHub>("/carthub");

app.Run();
