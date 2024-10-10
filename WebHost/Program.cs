using Microsoft.AspNetCore.Server.Kestrel.Core;
using s3ng.WebHost.Mappers;
using s3ng.WebHost.RegisterServiceClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//automapper
builder.Services.AddAutoMapper(typeof(RegistrationProfile));

var configuration = builder.Configuration;
builder.Services.AddRegistrationServiceClient(configuration);

//general
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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