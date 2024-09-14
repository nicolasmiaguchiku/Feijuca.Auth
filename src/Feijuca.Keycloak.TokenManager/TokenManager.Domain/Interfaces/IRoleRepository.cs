using TokenManager.Common.Models;

namespace TokenManager.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<Result> GetRolesForClientAsync(string tenant, string clientId);
    }
}
