using Feijuca.Auth.Common.Models;

namespace Feijuca.Auth.Common.Errors
{
    public static class ConfigErrors
    {
        public static string TechnicalMessage { get; private set; } = "";

        public static Error InsertConfig => new(
            "Config.InsertConfig",
            $"An error occurred while trying insert a new config {TechnicalMessage}"
        );

        public static Error NoConfigInserted => new(
            "Config.NoConfigInserted",
            $"There is no config inserted, please insert one config! {TechnicalMessage}"
        );

        public static void SetTechnicalMessage(string technicalMessage)
        {
            TechnicalMessage = technicalMessage;
        }
    }
}
