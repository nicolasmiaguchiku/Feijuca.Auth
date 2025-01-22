using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Config
{
    public class AddOrUpdateConfigCommandHandler(IConfigRepository configRepository) : IRequestHandler<AddOrUpdateConfigCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(AddOrUpdateConfigCommand request, CancellationToken cancellationToken)
        {
            bool result;
            var existingConfig = await configRepository.GetConfigAsync();
            if (existingConfig == null)
            {
                result = await configRepository.AddConfigAsync(request.Request.ToEntity(), cancellationToken);

                if (result)
                {
                    return Result<bool>.Success(true);
                }
            }

            var newRealm = new Models.Realm
            {
                Audience = request.Request.Realms!.First().Audience,
                DefaultSwaggerTokenGeneration = false,
                Issuer = request.Request.Realms!.First().Issuer,
                Name = request.Request.Realms!.First().Name,
            };

            existingConfig!.Realms = existingConfig.Realms!.Concat([newRealm]);

            result = await configRepository.UpdateRealmConfigAsync(existingConfig!.Id, existingConfig);

            if (result)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(ConfigErrors.InsertConfig);
        }
    }
}
