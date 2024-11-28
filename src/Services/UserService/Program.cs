using Microsoft.AspNetCore.Server.Kestrel.Core;
using UserService.EFCore;
using UserService.Mapping;
using UserService.Services;
using MediatR;
using UserService.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddAutoMapper(typeof(UserServiceMappingProfile));

builder.Services.AddScoped<IUserRepository, UserRepository>();

var configuration = builder.Configuration;

builder.Services.AddDbContext<UserServiceContext>(optionsBuilder
    => optionsBuilder
        .UseLazyLoadingProxies()
        .UseNpgsql(configuration.GetConnectionString("UserDb")));

builder.WebHost.ConfigureKestrel(options =>
{   //TODO
    options.ListenAnyIP(5005, listenOptions => listenOptions.Protocols = HttpProtocols.Http2);
});

var app = builder.Build();

app.MapGrpcService<UserManagerService>();

app.Run();