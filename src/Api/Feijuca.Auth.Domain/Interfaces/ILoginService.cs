using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;

namespace Feijuca.Auth.Domain.Interfaces
{
    public interface ILoginService
    {
        Task<Result<TokenDetails>> LoginAsync(bool revokeActiveSessions, string username, string password, CancellationToken cancellationToken);
    }
}
