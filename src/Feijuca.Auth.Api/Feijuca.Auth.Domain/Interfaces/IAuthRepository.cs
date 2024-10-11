using Common.Models;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<Result<TokenDetails>> GetAccessTokenAsync(string tenant);        
    }
}