using TokenManager.Application.Responses;
using TokenManager.Domain.Entities;

namespace TokenManager.Application.Mappers
{
    public static class GroupRolesMapper
    {
        public static IEnumerable<GroupRolesResponse> ToResponse(this IEnumerable<ClientMapping> clientMappings)
        {
            return clientMappings.Select(clientMapping =>
            {
                var roles = clientMapping.Mappings
                    .Select(mapping => new RoleResponse(
                        mapping.Id,
                        mapping.Name,
                        mapping.Description,
                        mapping.Composite,
                        mapping.ClientRole,
                        mapping.ContainerId
                    ));

                return new GroupRolesResponse(clientMapping.Id, clientMapping.Client, roles.ToList());
            });
        }
    }
}
