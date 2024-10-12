using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Application.Responses;
using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;

using MediatR;

namespace Feijuca.Auth.Application.Queries.GroupRoles
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
