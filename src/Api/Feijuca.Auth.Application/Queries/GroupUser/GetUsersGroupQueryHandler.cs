using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Application.Responses;
using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Providers;
using LiteBus.Queries.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Queries.GroupUser
{
    public class GetUsersGroupQueryHandler(IGroupRepository groupRepository,
        IUserRepository userRepository,
        ITenantProvider tenantService) : IQueryHandler<GetUsersGroupQuery, Result<Responses.PagedResult<UserGroupResponse>>>
    {
        private readonly IGroupRepository _groupRepository = groupRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITenantProvider _tenantProvider = tenantService;

        public async Task<Result<Responses.PagedResult<UserGroupResponse>>> HandleAsync(GetUsersGroupQuery request, CancellationToken cancellationToken = default)
        {
            var allGroupsResult = await _groupRepository.GetAllAsync(cancellationToken);

            if (allGroupsResult.IsSuccess)
            {
                var groupSearched = allGroupsResult.Data.FirstOrDefault(x => x.Id == request.GetUsersGroupRequest.GroupId);
                if (groupSearched != null)
                {
                    var totalUsers = await _userRepository.GetTotalAsync(cancellationToken);

                    var resultMembers = await _groupRepository.GetUsersInGroupAsync(
                        groupSearched.Id,
                        request.GetUsersGroupRequest.ToUserFilters(),
                        totalUsers,
                        cancellationToken);

                    var filteredUsers = resultMembers.Data.AsEnumerable();

                    if (request.GetUsersGroupRequest.Usernames?.Any() ?? false)
                    {
                        filteredUsers = resultMembers.Data
                            .Where(x => request.GetUsersGroupRequest.Usernames.Any(filter => x.Username.Contains(filter, StringComparison.OrdinalIgnoreCase)));
                    }

                    var usersInGroup = new UserGroupResponse(groupSearched.ToResponse(), filteredUsers.ToUsersResponse(_tenantProvider.Tenant.Name));

                    var result = usersInGroup.ToResponse(request.GetUsersGroupRequest.PageFilter, filteredUsers.Count());

                    return Result<Responses.PagedResult<UserGroupResponse>>.Success(result);
                }
            }

            return Result<Responses.PagedResult<UserGroupResponse>>.Failure(GroupErrors.GetUsersInGroupsError);
        }
    }
}
