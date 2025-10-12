using Feijuca.Auth.Infra.CrossCutting.Models;
using Mattioli.Configurations.Extensions.Loggings;
using Mattioli.Configurations.Extensions.Sentry;
using Mattioli.Configurations.Models;
using Microsoft.AspNetCore.Builder;

namespace Feijuca.Auth.Infra.CrossCutting.Extensions
{
    public static class MltSettingsExtensions
    {
        public static bool IsValidForSentry(this MltSettings? mlt)
        => mlt is not null &&
        !string.IsNullOrWhiteSpace(mlt.Dsn) &&
        !string.IsNullOrWhiteSpace(mlt.ApplicationName);

        public static bool IsValidForSerilog(this MltSettings? mlt, SeqSettings? seq)
        => mlt is not null && seq is not null &&
        !string.IsNullOrWhiteSpace(mlt.OpenTelemetryColectorUrl) &&
        !string.IsNullOrWhiteSpace(mlt.ApplicationName) &&
        !string.IsNullOrWhiteSpace(seq.Url);
    }

    public static class MltAndLoggingExtensions
    {
        public static WebApplicationBuilder ConfigureTelemetryAndLogging(this WebApplicationBuilder builder, Settings applicationSettings)
        {
            var mlt = applicationSettings.MltSettings;
            var seq = applicationSettings.SeqSettings;

            if (mlt is null)
                return builder;

            if (mlt.IsValidForSentry())
            {
                builder.UseMltSentry(mlt);
            }

            if (mlt.IsValidForSerilog(seq))
            {
                builder.Host.UseSerilog(
                mlt.OpenTelemetryColectorUrl!,
                mlt.ApplicationName!,
                seq!.Url!
                );
            }

            return builder;
        }
    }
}