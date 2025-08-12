﻿using Mattioli.Configurations.Models;

namespace Feijuca.Auth.Common.Errors
{
    public static class ClientScopesErrors
    {
        public static string TechnicalMessage { get; private set; } = "";

        public static Error CreateClientScopesError => new(
            "ClientScopes.CreateClientScopesError",
            $"An error occurred while trying create the client scopes {TechnicalMessage}"
        );
        
        public static Error CreateAudienceMapperProtocolError => new(
            "ClientScopes.CreateAudienceMapperProtocolError",
            $"An error occurred while trying create the client scope protocol audience mapper {TechnicalMessage}"
        );

        public static void SetTechnicalMessage(string technicalMessage)
        {
            TechnicalMessage = technicalMessage;
        }
    }
}
