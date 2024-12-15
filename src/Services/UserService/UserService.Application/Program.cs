using Microsoft.AspNetCore.Server.Kestrel.Core;
using UserService.Application.Mapping;
using UserService.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Repository;
using UserService.Infrastructure.EFCore;

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