namespace Feijuca.Auth.Domain.Interfaces
{
    public interface IRealmRepository : IBaseRepository
    {
        Task<string> GetRealmConfig(string name, CancellationToken cancellationToken);
    }
}
