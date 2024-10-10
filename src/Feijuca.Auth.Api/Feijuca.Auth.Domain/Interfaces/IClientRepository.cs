using Common.Models;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IClientRepository
    {
        Task<Result<IEnumerable<Client>>> GetClientsAsync(string tenant);
    }
}
