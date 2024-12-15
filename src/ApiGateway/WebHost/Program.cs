using Microsoft.AspNetCore.Server.Kestrel.Core;
using WebHost.Mappers;
using WebHost.RegisterServiceClient;
using WebHost.UserServiceClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//automapper
builder.Services.AddAutoMapper(typeof(IAMProfile));

//general
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpc();

var configuration = builder.Configuration;

builder.Services.AddRegistrationServiceClient(configuration);
builder.Services.AddUserServiceClient(configuration);

builder.WebHost.ConfigureKestrel(o =>
{
    o.ListenAnyIP(8080, listenOptions => listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3);
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

app.Run();
