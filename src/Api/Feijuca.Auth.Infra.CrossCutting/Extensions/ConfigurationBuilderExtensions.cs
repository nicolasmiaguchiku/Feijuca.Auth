using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Feijuca.Auth.Infra.CrossCutting.Models;

namespace Feijuca.Auth.Infra.CrossCutting.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static Settings ApplyEnvironmentOverridesToSettings(this IConfiguration configuration, IHostEnvironment env)
        {
            var settings = configuration.GetSection("Settings").Get<Settings>();

            if (!env.IsDevelopment())
            {
                settings!.MongoSettings.ConnectionString = GetEnvOrDefault("Feijuca_ConnectionString", settings.MongoSettings.ConnectionString);
                settings.MongoSettings.DatabaseName = GetEnvOrDefault("Feijuca_DatabaseName", settings.MongoSettings.DatabaseName);
                settings.MltSettings!.OpenTelemetryColectorUrl = GetEnvOrDefault("OpenTelemetryColectorUrl", settings.MltSettings!.OpenTelemetryColectorUrl);
                settings.MltSettings.Dsn = GetEnvOrDefault("SentrySettings_Dsn", settings.MltSettings.Dsn);
                settings.SeqSettings.Url = GetEnvOrDefault("SeqSettings_Url", settings.MltSettings.Dsn);
            }

            return settings!;
        }
        private static string GetEnvOrDefault(string key, string? fallback)
        {
            var value = Environment.GetEnvironmentVariable(key);
            return string.IsNullOrWhiteSpace(value) ? fallback ?? "" : value;
        }
    }
}
