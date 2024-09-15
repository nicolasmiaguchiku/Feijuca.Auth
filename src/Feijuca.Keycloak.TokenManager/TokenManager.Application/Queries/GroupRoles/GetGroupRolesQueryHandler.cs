using MediatR;

using TokenManager.Application.Mappers;
using TokenManager.Application.Responses;
using TokenManager.Common.Errors;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Queries.GroupRoles
{
    public class GetGroupRolesQueryHandler(IGroupRolesRepository roleGroupRepository) : IRequestHandler<GetGroupRolesQuery, Result<IEnumerable<GroupRolesResponse>>>
    {
        private readonly IGroupRolesRepository _roleGroupRepository = roleGroupRepository;

        public async Task<Result<IEnumerable<GroupRolesResponse>>> Handle(GetGroupRolesQuery request, CancellationToken cancellationToken)
        {
            var groupsRolesResult = await _roleGroupRepository.GetGroupRolesAsync(request.Tenant, request.GroupId);
            
            if (groupsRolesResult.IsSuccess)
            {
                return Result<IEnumerable<GroupRolesResponse>>.Success(groupsRolesResult.Data.ToResponse());
            }

            return Result<IEnumerable<GroupRolesResponse>>.Failure(GroupErrors.GetUsersInGroupsError);
        }
    }
}
