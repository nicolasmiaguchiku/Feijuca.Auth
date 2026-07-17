using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Providers;
using LiteBus.Commands.Abstractions;
using Mattioli.Configurations.Models;

namespace Feijuca.Auth.Application.Commands.RealmAttributes;

public class UpdateRealmAttributesCommandHandler(IRealmRepository realmRepository, ITenantProvider tenantProvider) : ICommandHandler<UpdateRealmAttributesCommand, Result>
{
    public async Task<Result> HandleAsync(UpdateRealmAttributesCommand command, CancellationToken cancellationToken = default)
    {
        var realm = await realmRepository.GetAsync(tenantProvider.Tenant.Name, cancellationToken);

        var oldAttributesUpdated = realm.Data.Attributes;

        foreach (var newAttribute in command.UpdateRealmAttributesRequest.Attributes)
        {
            if (oldAttributesUpdated.TryGetValue(newAttribute.Key, out string? _))
            {
                oldAttributesUpdated[newAttribute.Key] = newAttribute.Value;
            }
        }

        realm.Data.Attributes = oldAttributesUpdated;

        var result = await realmRepository.UpdateRealmAsync(tenantProvider.Tenant.Name, realm.Data, cancellationToken);

        if (result.IsSuccess)
        {
            return Result.Success();
        }

        return Result.Failure(result.Error);
    }
}
