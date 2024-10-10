using DotNetEnv;
using DotNetEnv.Configuration;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using s3ng.IAM.DAL;
using s3ng.IAM.Seedwork;
using s3ng.IAM.Seedwork.Abstractions;
using s3ng.IAM.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddTransient<IHashCalculator, SHAHashCalculator>();

builder.Configuration.AddDotNetEnvMulti([".env" ], LoadOptions.TraversePath());
builder.Services.ConfigureContext(builder.Configuration);
builder.WebHost.ConfigureListen(builder.Configuration);

var app = builder.Build();

app.MapGrpcService<RegistrationService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();