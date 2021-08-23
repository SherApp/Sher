using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Sher.Application.Access.Jwt;
using Sher.Infrastructure.Data;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Sher.Infrastructure
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)));

        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection("JwtOptions").Get<JwtOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    ApplyDefaultJwtOptions(opt, options);
                })
                .AddJwtBearer("BearerAllowExpired", opt =>
                {
                    ApplyDefaultJwtOptions(opt, options);
                    opt.TokenValidationParameters.ValidateLifetime = false;
                });
            
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("TokenRefresh",
                    cfg => cfg.AddAuthenticationSchemes("BearerAllowExpired").RequireAuthenticatedUser());
            });

            return services;
        }

        private static void ApplyDefaultJwtOptions(JwtBearerOptions bearerOptions, JwtOptions jwtValidationOptions)
        {
            bearerOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtValidationOptions.SecurityKey)),
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidIssuer = jwtValidationOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtValidationOptions.Audience
            };
            bearerOptions.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    if (!context.Request.Headers.ContainsKey(HeaderNames.Authorization))
                    {
                        context.Token = context.Request.Cookies["JwtToken"];
                    }
                    return Task.CompletedTask;
                }
            };
        }
    }
}