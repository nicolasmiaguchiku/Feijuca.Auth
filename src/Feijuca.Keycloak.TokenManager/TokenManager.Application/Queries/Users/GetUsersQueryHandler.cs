using MediatR;
using TokenManager.Application.Mappers;
using TokenManager.Application.Responses;
using TokenManager.Common.Errors;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Queries.Users
{
    public class GetUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUsersQuery, Result<PagedResult<UserResponse>>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<PagedResult<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUsersAsync(request.Tenant, request.GetUsersRequest.ToUserFilters());

            if (result.IsSuccess)
            {
                var totalUsers = await _userRepository.GetTotalUsersAsync(request.Tenant);
                return Result<PagedResult<UserResponse>>.Success(result.Response.ToUserResponse(request.GetUsersRequest.PageFilter, totalUsers));
            }

            return Result<PagedResult<UserResponse>>.Failure(UserErrors.GetAllUsersError);
        }
    }
}
