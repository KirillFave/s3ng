using Microsoft.AspNetCore.Server.Kestrel.Core;
using WebHost.OrderServiceConfiguration;
using WebHost.UserServiceClient;

namespace WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.Sources.Clear();
            builder.Configuration.AddJsonFile("appsettings.json");
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddGrpc();

            var configuration = builder.Configuration;
            builder.Services.AddUserServiceClient(configuration);
            builder.Services.ConfigureOrderService(configuration);

            builder.WebHost.ConfigureKestrel(o =>
            {
                o.ListenAnyIP(8080, listenOptions => listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3);
            });

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
                options.Title = "API Doc";
                options.Version = "1.0";
            });

            builder.Services.AddAutoMapper(typeof(Program));

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
        }
    }
}