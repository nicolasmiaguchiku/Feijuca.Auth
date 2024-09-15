using MediatR;
using TokenManager.Application.Mappers;
using TokenManager.Application.Responses;
using TokenManager.Common.Errors;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Queries.GroupUser
{
    public class GetUsersGroupQueryHandler(IGroupRepository groupRepository) : IRequestHandler<GetUsersGroupQuery, Result<UserGroupResponse>>
    {
        private readonly IGroupRepository _groupRepository = groupRepository;

        public async Task<Result<UserGroupResponse>> Handle(GetUsersGroupQuery request, CancellationToken cancellationToken)
        {
            var result = await _groupRepository.GetAllAsync(request.Tenant);

            if (result.IsSuccess)
            {
                var groupSearched = result.Data.FirstOrDefault(x => x.Id == request.GroupId);
                if (groupSearched != null)
                {
                    var resultMembers = await _groupRepository.GetUsersInGroupAsync(request.Tenant, groupSearched.Id);
                    if (resultMembers.IsSuccess)
                    {
                        var usersInGroup = new UserGroupResponse(groupSearched.ToResponse(), resultMembers.Data.ToResponse());
                        return Result<UserGroupResponse>.Success(usersInGroup);
                    }
                }
            }

            return Result<UserGroupResponse>.Failure(GroupErrors.GetUsersInGroupsError);
        }
    }
}
