using TokenManager.Common.Models;
using TokenManager.Domain.Entities;

namespace TokenManager.Domain.Interfaces
{
    public interface IClientRepository
    {
        Task<Result<IEnumerable<Client>>> GetClientsAsync(string tenant);
    }
}
