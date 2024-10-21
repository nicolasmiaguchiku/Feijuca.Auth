using Feijuca.Auth.Domain.Entities;

namespace Feijuca.Auth.Domain.Interfaces
{
    public interface IConfigRepository
    {
        Task<bool> AddConfigAsync(AuthSettingsEntity newConfig);
        AuthSettingsEntity GetConfig();
    }
}
