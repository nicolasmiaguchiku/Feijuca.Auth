using MediatR;

using TokenManager.Application.Mappers;
using TokenManager.Application.Responses;
using TokenManager.Common.Errors;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Queries.Users
{
    public class GetAllUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserResponse>>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<IEnumerable<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetAllAsync(request.Tenant);

            if (result.IsSuccess)
            {
                return Result<IEnumerable<UserResponse>>.Success(result.Data.ToResponse());
            }

            return Result<IEnumerable<UserResponse>>.Failure(UserErrors.GetAllUsersError);
        }
    }
}
