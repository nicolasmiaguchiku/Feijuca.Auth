using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;

namespace Feijuca.Auth.Domain.Interfaces
{
    public interface IClientRepository
    {
        Task<Result<IEnumerable<Client>>> GetClientsAsync(string tenant);
    }
}
