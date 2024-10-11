using Common.Errors;
using Common.Models;
using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Services
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
