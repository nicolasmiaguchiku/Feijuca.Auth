using MediatR;
using TokenManager.Common.Errors;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Commands.GroupRoles;

public class RemoveRoleFromGroupCommandHandler(IGroupRepository groupRepository, IGroupRolesRepository roleGroupRepository, IRoleRepository roleRepository) : IRequestHandler<RemoveRoleFromGroupCommand, Result<bool>>
{
    private readonly IGroupRepository _groupRepository = groupRepository;
    private readonly IGroupRolesRepository _roleGroupRepository = roleGroupRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<Result<bool>> Handle(RemoveRoleFromGroupCommand command, CancellationToken cancellationToken)
    {
        var groupsResult = await _groupRepository.GetAllAsync(command.Tenant);
        if (groupsResult.IsSuccess && groupsResult.Data.Any(x => x.Id == command.RemoveRoleFromGroupRequest.GroupId))
        {
            var rolesResult = await _roleRepository.GetRolesForClientAsync(command.Tenant, command.RemoveRoleFromGroupRequest.ClientId);
            if (rolesResult.IsSuccess && rolesResult.Data.Any(x => x.Id == command.RemoveRoleFromGroupRequest.RoleId))
            {
                await _roleGroupRepository.RemoveRoleFromGroupAsync(command.Tenant, 
                    command.RemoveRoleFromGroupRequest.ClientId, 
                    command.RemoveRoleFromGroupRequest.GroupId, 
                    command.RemoveRoleFromGroupRequest.RoleId);
            }
        }

        return Result<bool>.Failure(GroupRolesErrors.RemovingRoleFromGroupError);
    }
}
