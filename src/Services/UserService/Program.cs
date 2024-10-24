using DotNetEnv;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using UserService.EFCore;
using UserService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

Env.Load();

var configuration = builder.Configuration;
builder.Services.ConfigureContext(configuration);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5005, listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
});

var app = builder.Build();

app.MapGrpcService<UserManagerService>();

app.Run();