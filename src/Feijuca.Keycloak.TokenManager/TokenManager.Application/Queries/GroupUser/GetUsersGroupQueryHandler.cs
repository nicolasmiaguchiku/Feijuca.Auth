using MediatR;

using TokenManager.Application.Mappers;
using TokenManager.Application.Responses;
using TokenManager.Common.Errors;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Queries.GroupUser
{
    public class GetUsersGroupQueryHandler(IGroupRepository groupRepository, IUserRepository userRepository) : IRequestHandler<GetUsersGroupQuery, Result<PagedResult<UserGroupResponse>>>
    {
        private readonly IGroupRepository _groupRepository = groupRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<PagedResult<UserGroupResponse>>> Handle(GetUsersGroupQuery request, CancellationToken cancellationToken)
        {
            var allGroupsResult = await _groupRepository.GetAllAsync(request.Tenant);

            if (allGroupsResult.IsSuccess)
            {
                var groupSearched = allGroupsResult.Data.FirstOrDefault(x => x.Id == request.GetUsersGroupRequest.GroupId);
                if (groupSearched != null)
                {
                    var resultMembers = await _groupRepository.GetUsersInGroupAsync(request.Tenant,
                        groupSearched.Id,
                        request.GetUsersGroupRequest.ToUserFilters());

                    var usersInGroup = new UserGroupResponse(groupSearched.ToResponse(), resultMembers.Data.ToUsersResponse());
                    var totalUsers = await _userRepository.GetTotalUsersAsync(request.Tenant);
                    var result = usersInGroup.ToResponse(request.GetUsersGroupRequest.PageFilter, totalUsers);
                    return Result<PagedResult<UserGroupResponse>>.Success(result);
                }
            }

            return Result<PagedResult<UserGroupResponse>>.Failure(GroupErrors.GetUsersInGroupsError);
        }
    }
}
