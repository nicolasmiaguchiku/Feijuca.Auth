﻿using Mattioli.Configurations.Models;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Filters;

namespace Feijuca.Auth.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        Task<Result<IEnumerable<User>>> GetAllAsync(UserFilters userFilters, int totalUsers, CancellationToken cancellationToken);
        Task<int> GetTotalAsync(CancellationToken cancellationToken);
        Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<Result<bool>> CreateAsync(string tenant, User user, CancellationToken cancellationToken);
        Task<Result<bool>> UpdateUserAsync(Guid id, User user, CancellationToken cancellationToken);
        Task<Result<bool>> ResetPasswordAsync(Guid id, string tenant, string password, CancellationToken cancellationToken);
        Task<Result<bool>> RevokeSessionsAsync(Guid id, CancellationToken cancellationToken);
        Task<Result<User>> GetAsync(string tenant, string username, CancellationToken cancellationToken);
        Task<Result> SendEmailVerificationAsync(string userId, CancellationToken cancellationToken);
        Task<Result<TokenDetails>> LoginAsync(string username, string password, CancellationToken cancellationToken);
        Task<Result<bool>> SignoutAsync(string refreshToken, CancellationToken cancellationToken);
        Task<Result<TokenDetails>> RefreshTokenAsync(string tenant, string refreshToken, CancellationToken cancellationToken);
    }
}
