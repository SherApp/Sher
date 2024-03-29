using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Sher.Infrastructure.Data;
using Sher.IntegrationTests.Utils;

namespace Sher.IntegrationTests
{
    public class TestServerFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(cfgBuilder =>
            {
                cfgBuilder.AddTestSources();
            });

            builder.ConfigureServices(services =>
            {
                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();

                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                
                db.Database.EnsureCreated();
            });
        }
    }
}