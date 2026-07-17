using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Providers;
using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.Client
{
    public class AddClientCommandHandler(IClientRepository clientRepository, ITenantProvider tenantService) : ICommandHandler<AddClientCommand, Result>
    {
        public async Task<Result> HandleAsync(AddClientCommand request, CancellationToken cancellationToken)
        {
            var client = request.AddClientRequest.ToClientEntity();
            var result = await clientRepository.CreateClientAsync(client, tenantService.Tenant.Name, cancellationToken);

            if (result.IsFailure)
            {
                return Result.Failure(ClientErrors.CreateClientError);
            }

            return Result.Success();
        }
    }
}