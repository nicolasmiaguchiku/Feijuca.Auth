using TokenManager.Common.Models;
using TokenManager.Domain.Entities;

namespace TokenManager.Domain.Interfaces
{
    public interface ITokenRepository
    {
        Task<Result<TokenDetails>> GetAccessTokenAsync(string tenant);
    }
}
