using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Providers;
using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.ClientScopeMapper
{
    public class AddClientScopeMapperCommandHandler(IClientScopesRepository clientScopesRepository, ITenantProvider tenantProvider) : ICommandHandler<AddClientScopeMapperCommand, Result<bool>>
    {
        public async Task<Result<bool>> HandleAsync(AddClientScopeMapperCommand request, CancellationToken cancellationToken)
        {
            var result = await clientScopesRepository.AddUserPropertyMapperAsync(request.ClientScopeId, 
                request.UserPropertyName, 
                request.ClaimName,
                tenantProvider.Tenant.Name,
                cancellationToken);

            if (result)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(ClientScopesErrors.CreateAudienceMapperProtocolError);
        }
    }
}
