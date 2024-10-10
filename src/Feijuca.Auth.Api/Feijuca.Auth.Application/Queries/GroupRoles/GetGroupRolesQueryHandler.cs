using Application.Mappers;
using Application.Responses;
using Common.Errors;
using Common.Models;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries.GroupRoles
{
    public class GetGroupRolesQueryHandler(IGroupRolesRepository roleGroupRepository) : IRequestHandler<GetGroupRolesQuery, Result<IEnumerable<GroupRolesResponse>>>
    {
        private readonly IGroupRolesRepository _roleGroupRepository = roleGroupRepository;

        public async Task<Result<IEnumerable<GroupRolesResponse>>> Handle(GetGroupRolesQuery request, CancellationToken cancellationToken)
        {
            var groupsRolesResult = await _roleGroupRepository.GetGroupRolesAsync(request.Tenant, request.GroupId);

            if (groupsRolesResult.IsSuccess)
            {
                return Result<IEnumerable<GroupRolesResponse>>.Success(groupsRolesResult.Response.ToResponse());
            }

            return Result<IEnumerable<GroupRolesResponse>>.Failure(GroupErrors.GetUsersInGroupsError);
        }
    }
}
