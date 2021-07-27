using System.IO;
using Microsoft.Extensions.Configuration;

namespace Sher.IntegrationTests.Utils
{
    public static class ConfigExtensions
    {
        public static IConfigurationBuilder AddTestSources(this IConfigurationBuilder builder)
            => builder.AddJsonFile(GetAppSettingsPath())
                .AddEnvironmentVariables();

        public static string GetAppSettingsPath()
        {
            var projectDir = Directory.GetCurrentDirectory();
            return Path.Combine(projectDir, "appsettings.json");
        }
    }
}