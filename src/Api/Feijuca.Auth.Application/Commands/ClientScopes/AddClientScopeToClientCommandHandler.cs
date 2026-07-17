using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Providers;
using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.ClientScopes
{
    public class AddClientScopeToClientCommandHandler(IClientScopesRepository clientScopesRepository, ITenantProvider tenantProvider) : ICommandHandler<AddClientScopeToClientCommand, Result<bool>>
    {
        public async Task<Result<bool>> HandleAsync(AddClientScopeToClientCommand request, CancellationToken cancellationToken)
        {
            var result = await clientScopesRepository.AddClientScopeToClientAsync(
                request.AddClientScopeToClientRequest.ClientId,
                tenantProvider.Tenant.Name,
                request.AddClientScopeToClientRequest.ClientScopeId,
                request.AddClientScopeToClientRequest.IsOpticionalScope, 
                cancellationToken);

            if(result)
                return Result<bool>.Success(true);

            return Result<bool>.Failure(ClientErrors.AddClientRoleError);
        }
    }
}
