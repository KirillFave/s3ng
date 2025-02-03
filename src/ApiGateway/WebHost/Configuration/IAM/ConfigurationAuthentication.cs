using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.IAM.JWT;

namespace WebHost.Configuration.IAMConfiguration
{
    public static class ConfigurationAuthentication
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                var jwtSection = configuration.GetSection(JwtOptions.Jwt);
                var jwtOptions = jwtSection.Get<JwtOptions>();
                if (jwtOptions is null)
                    return;

                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(t =>
                    {
                        t.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = jwtOptions.Issuer,
                            ValidateAudience = true,
                            ValidAudience = jwtOptions.Audience,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                        };

                        t.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                Console.WriteLine($"Token validation failed: {context.Exception.Message}");
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = context =>
                            {
                                Console.WriteLine("Token validated successfully");
                                return Task.CompletedTask;
                            }
                        };
                    });

                services.AddAuthorization();
            }
            catch
            {
                throw;
            }
        }
    }
}
