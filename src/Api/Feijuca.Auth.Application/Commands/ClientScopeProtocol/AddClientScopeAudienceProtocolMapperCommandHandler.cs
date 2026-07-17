using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Providers;
using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.ClientScopeProtocol
{
    public class AddClientScopeAudienceProtocolMapperCommandHandler(IClientScopesRepository clientScopesRepository, ITenantProvider tenantProvider) : ICommandHandler<AddClientScopeAudienceProtocolMapperCommand, Result<bool>>
    {
        public async Task<Result<bool>> HandleAsync(AddClientScopeAudienceProtocolMapperCommand request, CancellationToken cancellationToken)
        {
            var result = await clientScopesRepository.AddAudienceMapperAsync(request.ClientScopeId, tenantProvider.Tenant.Name, cancellationToken);

            if (result)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(ClientScopesErrors.CreateAudienceMapperProtocolError);
        }
    }
}
