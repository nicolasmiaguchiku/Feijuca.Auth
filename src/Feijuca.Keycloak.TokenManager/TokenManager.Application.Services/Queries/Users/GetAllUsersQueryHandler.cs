using MediatR;

using TokenManager.Common.Errors;
using TokenManager.Common.Models;
using TokenManager.Domain.Entities;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Queries.Users
{
    public class GetAllUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<User>>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<IEnumerable<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetAllAsync(request.Tenant);

            if (result.IsSuccess)
            {
                return Result<IEnumerable<User>>.Success(result.Data);
            }

            return Result<IEnumerable<User>>.Failure(UserErrors.GetAllUsersError);
        }
    }
}
