using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;

namespace Feijuca.Auth.Domain.Interfaces
{
    public interface IConfigRepository
    {
        Task<bool> AddConfigAsync(KeycloakSettingsEntity newConfig);
        KeycloakSettings GetConfig();
    }
}
