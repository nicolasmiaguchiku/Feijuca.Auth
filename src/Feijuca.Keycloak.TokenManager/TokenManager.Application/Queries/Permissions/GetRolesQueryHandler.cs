using MediatR;

using TokenManager.Application.Responses;
using TokenManager.Common.Errors;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Queries.Permissions
{
    public class GetRolesQueryHandler(IClientRepository clientRepository, IRoleRepository roleRepository) : IRequestHandler<GetRolesQuery, Result<IEnumerable<ClientRoleResponse>>>
    {
        private readonly IClientRepository _clientRepository = clientRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task<Result<IEnumerable<ClientRoleResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var result = await _clientRepository.GetClientsAsync(request.Tenant);
            if (result.IsSuccess)
            {
                var roleResponse = new List<ClientRoleResponse>();
                foreach (var client in result.Response)
                {
                    var rolesResult = await _roleRepository.GetRolesForClientAsync(request.Tenant, client.Id);

                    if (rolesResult.IsSuccess)
                    {
                        var rolesResponse = rolesResult.Response.Select(x => new RoleResponse(x.Id, x.Name, x.Description ?? "", false, false, string.Empty));
                        roleResponse.Add(new ClientRoleResponse(client.ClientId, client.Id, rolesResponse));
                    }
                }

                return Result<IEnumerable<ClientRoleResponse>>.Success(roleResponse);
            }

            return Result<IEnumerable<ClientRoleResponse>>.Failure(RoleErrors.GetRoleErrors);
        }
    }
}
