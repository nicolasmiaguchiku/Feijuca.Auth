using Feijuca.Auth.Common.Models;

namespace Feijuca.Auth.Common.Errors
{
    public static class ClientErrors
    {
        public static string TechnicalMessage { get; private set; } = "";

        public static Error GetClientsErrors => new(
            "Clients.GetClientsErrors",
            $"An error occurred while trying get clients {TechnicalMessage}"
        );
        
        public static Error CreateClientError => new(
            "Clients.CreateClientError",
            $"An error occurred while trying create a new client {TechnicalMessage}"
        );
        
        public static Error AddClientRoleError => new(
            "Clients.AddClientRoleError",
            $"An error occurred while trying add a new client role {TechnicalMessage}"
        );

        public static void SetTechnicalMessage(string technicalMessage)
        {
            TechnicalMessage = technicalMessage;
        }
    }
}
