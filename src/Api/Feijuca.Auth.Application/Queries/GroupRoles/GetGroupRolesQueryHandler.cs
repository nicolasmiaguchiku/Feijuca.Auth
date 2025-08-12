﻿using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Common.Errors;
using Mattioli.Configurations.Models;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;
using Feijuca.Auth.Application.Responses;

namespace Feijuca.Auth.Application.Queries.GroupRoles
{
    public class GetGroupRolesQueryHandler(IGroupRolesRepository roleGroupRepository) : IRequestHandler<GetGroupRolesQuery, Result<IEnumerable<GroupRolesResponse>>>
    {
        private readonly IGroupRolesRepository _roleGroupRepository = roleGroupRepository;

        public async Task<Result<IEnumerable<GroupRolesResponse>>> Handle(GetGroupRolesQuery request, CancellationToken cancellationToken)
        {
            var groupsRolesResult = await _roleGroupRepository.GetGroupRolesAsync(request.GroupId, cancellationToken);

            if (groupsRolesResult.IsSuccess)
            {
                return Result<IEnumerable<GroupRolesResponse>>.Success(groupsRolesResult.Data.ToResponse());
            }

            return Result<IEnumerable<GroupRolesResponse>>.Failure(GroupErrors.GetUsersInGroupsError);
        }
    }
}
