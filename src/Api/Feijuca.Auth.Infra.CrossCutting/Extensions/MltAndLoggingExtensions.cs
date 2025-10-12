using Feijuca.Auth.Infra.CrossCutting.Models;
using Mattioli.Configurations.Extensions.Loggings;
using Mattioli.Configurations.Extensions.Sentry;
using Mattioli.Configurations.Models;
using Microsoft.AspNetCore.Builder;

namespace Feijuca.Auth.Infra.CrossCutting.Extensions
{
    public static class TelemetryExtensions
    {
        public static bool IsValidForSentry(this MltSettings? mlt) => mlt is { Dsn: not null or "", ApplicationName: not null or "" };

        public static bool IsValidForSerilog(this MltSettings? mlt, SeqSettings? seq) =>
            mlt is { OpenTelemetryColectorUrl: not null or "", ApplicationName: not null or "" }
            && seq is { Url: not null or "" };

        public static WebApplicationBuilder ConfigureTelemetryAndLogging(
            this WebApplicationBuilder builder,
            Settings applicationSettings)
        {
            if (applicationSettings.MltSettings is not { } mlt)
            {
                return builder;
            }

            var seq = applicationSettings.SeqSettings;

            if (mlt.IsValidForSentry())
            {
                builder.UseMltSentry(mlt);
            }

            if (mlt.IsValidForSerilog(seq))
            {
                builder.Host.UseSerilog(mlt.OpenTelemetryColectorUrl, mlt.ApplicationName, seq.Url);
            }

            return builder;
        }
    }
}
