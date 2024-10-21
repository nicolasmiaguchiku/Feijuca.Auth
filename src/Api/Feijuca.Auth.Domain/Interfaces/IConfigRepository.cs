using Feijuca.Auth.Common.Models;

namespace Feijuca.Auth.Domain.Interfaces
{
    public interface IConfigRepository
    {
        Task<bool> AddConfigAsync(KeycloakSettings newConfig);
        KeycloakSettings GetConfig();
    }
}
