using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Domain.Interfaces
{
    public interface IRealmRepository : IBaseRepository
    {
        Task<IEnumerable<RealmEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<RealmEntity>> GetAsync(string name, CancellationToken cancellationToken);
        Task<string> GetRealmConfigAsync(string name, CancellationToken cancellationToken);
        Task<bool> DeleteRealmAsync(string name, CancellationToken cancellationToken);
        Task<bool> CreateRealmAsync(RealmEntity realm, CancellationToken cancellationToken);
        Task<Result> UpdateRealmAsync(string name, RealmEntity realm, CancellationToken cancellationToken);
        Task<bool> UpdateRealmUnmanagedAttributePolicyAsync(string realmName, CancellationToken cancellationToken);
        Task<bool> EnableDisableRealmAsync(string name, bool enable, CancellationToken cancellationToken);
    }
}
