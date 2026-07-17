using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Domain.Interfaces;
using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Models;

namespace Feijuca.Auth.Application.Queries.Realm
{
    public class EnableRealmCommandHandler(IRealmRepository realmRepository) : ICommandHandler<EnableRealmCommand, Result<bool>>
    {
        public async Task<Result<bool>> HandleAsync(EnableRealmCommand request, CancellationToken cancellationToken)
        {
            var result = await realmRepository.EnableDisableRealmAsync(request.EnableRealmRequest.Realm, 
                request.EnableRealmRequest.Enable, 
                cancellationToken);

            if (result)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(RealmErrors.DeleteRealmError);
        }
    }
}
