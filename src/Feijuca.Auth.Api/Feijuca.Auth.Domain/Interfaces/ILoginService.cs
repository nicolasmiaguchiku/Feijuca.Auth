using Common.Models;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ILoginService
    {
        Task<Result<TokenDetails>> LoginAsync(bool revokeActiveSessions, string username, string password);
    }
}
