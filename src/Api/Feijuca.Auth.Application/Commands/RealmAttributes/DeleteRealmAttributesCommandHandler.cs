using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Providers;
using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Commands.RealmAttributes;

public class DeleteRealmAttributesCommandHandler(IRealmRepository realmRepository, ITenantProvider tenantProvider) : ICommandHandler<DeleteRealmAttributesCommand, Result>
{
    public async Task<Result> HandleAsync(DeleteRealmAttributesCommand command, CancellationToken cancellationToken = default)
    {
        var realm = await realmRepository.GetAsync(tenantProvider.Tenant.Name, cancellationToken);
        if (realm.IsSuccess)
        {
            var realmAttributes = realm.Data.Attributes;
            foreach (var key in command.DeleteRealmAttributeRequest)
            {
                realmAttributes.Remove(key);
            }

            realm.Data.Attributes = realmAttributes;

            var result = await realmRepository.UpdateRealmAsync(tenantProvider.Tenant.Name, realm.Data, cancellationToken);

            if (result.IsSuccess)
            {
                return Result.Success();
            }
        }

        return Result.Failure(realm.Error);
    }
}
