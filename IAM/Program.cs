using s3ng.IAM.DAL;
using s3ng.IAM.Seedwork;
using s3ng.IAM.Seedwork.Abstractions;
using s3ng.IAM.Services;
using Microsoft.EntityFrameworkCore;

namespace s3ng.IAM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGrpc();
            builder.Services.AddTransient<IHashCalculator, SHAHashCalculator>();
            var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            builder.Services.AddDbContext<DatabaseContext>(
                optionsBuilder => optionsBuilder.UseNpgsql(connectionString));

            var app = builder.Build();

            app.MapGrpcService<RegistrationService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}