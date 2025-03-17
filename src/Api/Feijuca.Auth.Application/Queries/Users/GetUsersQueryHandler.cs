using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Application.Responses;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Queries.Users
{
    public class GetUsersQueryHandler(IUserRepository userRepository, ITenantService tenantService) : IRequestHandler<GetUsersQuery, Result<PagedResult<UserResponse>>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITenantService _tenantService = tenantService;

        public async Task<Result<PagedResult<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetAllAsync(request.GetUsersRequest.ToUserFilters(), cancellationToken);

            if (!result.IsSuccess)
            {
                return Result<PagedResult<UserResponse>>.Failure(UserErrors.GetAllUsersError);
            }

            var filteredUsers = result.Response.AsEnumerable();

            if (request.GetUsersRequest.Usernames?.Any() ?? false)
            {
                filteredUsers = result.Response
                    .Where(x => request.GetUsersRequest.Usernames.Any(filter => x.Username.Contains(filter, StringComparison.OrdinalIgnoreCase)));
            }

            if (request.GetUsersRequest.AttributeKeys?.Count() == request.GetUsersRequest.AttributeValues?.Count())
            {
                for (int i = 0; i < request.GetUsersRequest.AttributeKeys?.Count(); i++)
                {
                    var key = request.GetUsersRequest.AttributeKeys?.ElementAt(i);
                    var value = request.GetUsersRequest.AttributeValues?.ElementAt(i);

                    filteredUsers = filteredUsers
                        .Where(u =>
                        {
                            u.Attributes.TryGetValue(key ?? "", out var values);
                            return values?.Any(v => v.Contains(value ?? "", StringComparison.OrdinalIgnoreCase)) ?? false;
                        });
                }
            }

            var users = filteredUsers.ToList();
            var totalUsers = users.Count;
            return Result<PagedResult<UserResponse>>.Success(users.ToUserResponse(request.GetUsersRequest.PageFilter, _tenantService.Tenant, totalUsers));
        }


    }
}
