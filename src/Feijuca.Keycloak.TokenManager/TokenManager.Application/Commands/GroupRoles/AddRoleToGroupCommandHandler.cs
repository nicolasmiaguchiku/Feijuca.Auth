using MediatR;
using TokenManager.Common.Errors;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Commands.GroupRoles;

public class AddRoleToGroupCommandHandler(IGroupRepository groupRepository, IGroupRolesRepository roleGroupRepository, IRoleRepository roleRepository) : IRequestHandler<AddRoleToGroupCommand, Result<bool>>
{
    private readonly IGroupRepository _groupRepository = groupRepository;
    private readonly IGroupRolesRepository _roleGroupRepository = roleGroupRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<Result<bool>> Handle(AddRoleToGroupCommand request, CancellationToken cancellationToken)
    {
        var groupsResult = await _groupRepository.GetAllAsync(request.Tenant);
        var rolesResult = await _roleRepository.GetRolesForClientAsync(request.Tenant, request.AddRoleToGroupRequest.ClientId);

        if (groupsResult.IsSuccess && rolesResult.IsSuccess)
        {
            var group = groupsResult.Data.FirstOrDefault(x => x.Id == request.AddRoleToGroupRequest.GroupId);
            var role = rolesResult.Data.FirstOrDefault(x => x.Id == request.AddRoleToGroupRequest.RoleId);

            if (group != null && role != null)
            {

                var result = await _roleGroupRepository.AddRoleToGroupAsync(request.Tenant,
                    group.Id,
                    request.AddRoleToGroupRequest.ClientId,
                    role.Id,
                    role.Name);

                if (result.IsSuccess)
                {
                    return Result<bool>.Success(true);
                }
            }
        }

        return Result<bool>.Failure(GroupRolesErrors.ErrorAddRoleToGroup);
    }
}
