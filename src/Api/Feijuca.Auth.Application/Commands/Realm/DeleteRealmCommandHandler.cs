using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Domain.Interfaces;
using Mattioli.Configurations.Models;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Realm
{
    public class DeleteRealmCommandHandler(IRealmRepository realmRepository) : IRequestHandler<DeleteRealmCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteRealmCommand request, CancellationToken cancellationToken)
        {
            var result = await realmRepository.DeleteRealmAsync(request.RealmName, cancellationToken);

            if (result)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(RealmErrors.DeleteRealmError);
        }
    }
}
