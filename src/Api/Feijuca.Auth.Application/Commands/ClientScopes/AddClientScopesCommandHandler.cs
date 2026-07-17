using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Providers;
using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.ClientScopes;

public class AddClientScopesCommandHandler(IClientScopesRepository clientScopesRepository, ITenantProvider tenantService) : ICommandHandler<AddClientScopesCommand, Result<bool>>
{
    public async Task<Result<bool>> HandleAsync(AddClientScopesCommand command, CancellationToken cancellationToken)
    {
        foreach (var clientScope in command.AddClientScopesRequest)
        {
            var scopeEntity = clientScope.ToClientScopesEntity();
            var result = await clientScopesRepository.AddClientScopesAsync(scopeEntity, tenantService.Tenant.Name, cancellationToken);

            if (string.IsNullOrEmpty(result))
            {
                return Result<bool>.Failure(ClientScopesErrors.CreateClientScopesError);
            }
        }


        return Result<bool>.Success(true);
    }
}
