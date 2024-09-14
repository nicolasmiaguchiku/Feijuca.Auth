using MediatR;
using TokenManager.Common.Models;
using TokenManager.Domain.Entities;
using TokenManager.Domain.Errors;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Queries
{
    public class GetAllUsersQueryHandler(IUserRepository userRepository, ITokenRepository tokenRepository) : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<User>>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenRepository _tokenRepository = tokenRepository;

        public async Task<Result<IEnumerable<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var accessTokenResult = await _tokenRepository.GetAccessTokenAsync(request.Tenant);
            if (accessTokenResult.IsSuccess)
            {
                var accessToken = accessTokenResult.Data.AccessToken;
                var result = await _userRepository.GetAllUsers(request.Tenant, accessToken);

                if (result.IsSuccess)
                {
                    return Result<IEnumerable<User>>.Success(result.Data);
                }                
            }

            return Result<IEnumerable<User>>.Failure(UserErrors.GetAllUsersError);
        }
    }
}
