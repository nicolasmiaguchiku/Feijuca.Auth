using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Interfaces;

namespace Feijuca.Auth.Domain.Services
{
    public class LoginService(IUserRepository userRepository) : ILoginService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<TokenDetails>> LoginAsync(bool revokeActiveSessions, string username, string password)
        {
            var user = await _userRepository.GetAsync(username);
            if (user.IsFailure)
            {
                return Result<TokenDetails>.Failure(UserErrors.InvalidUserNameOrPasswordError);
            }

            if (revokeActiveSessions)
            {
                await _userRepository.RevokeSessionsAsync(user.Response.Id);
            }

            return await _userRepository.LoginAsync(username, password);
        }

    }
}
