using Feijuca.Auth.Infra.CrossCutting.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Feijuca.Auth.Infra.CrossCutting.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static MongoSettings GetApplicationSettings(this IConfiguration configuration, IHostEnvironment env)
        {
            var settings = configuration.GetSection("Settings").Get<MongoSettings>()!;

            if (!env.IsDevelopment())
            {
                settings.ConnectionString = GetEnvironmentVariables("ConnectionString");
            }

            return settings!;
        }

        private static string GetEnvironmentVariables(string variableName)
        {
            return Environment.GetEnvironmentVariable(variableName) ?? "";
        }
    }
}
