using DotNetEnv;
using DotNetEnv.Configuration;
using IAM.DAL;
using IAM.Seedwork;
using IAM.Seedwork.Abstractions;
using IAM.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddTransient<IHashCalculator, SHAHashCalculator>();
builder.Services.AddTransient<ITokenProvider, JwtTokenProvider>();

builder.Configuration.AddDotNetEnvMulti([".env" ], LoadOptions.TraversePath());
builder.Services.ConfigureContext(builder.Configuration);
builder.WebHost.ConfigureListen(builder.Configuration);

var jwtSection = builder.Configuration.GetSection(JwtOptions.Jwt);
builder.Services.Configure<JwtOptions>(jwtSection);

var app = builder.Build();

app.MapGrpcService<RegistrationService>();
app.MapGrpcService<AuthenticationService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
