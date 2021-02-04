using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sher.Infrastructure.Data;

namespace Sher.Infrastructure
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)));
    }
}