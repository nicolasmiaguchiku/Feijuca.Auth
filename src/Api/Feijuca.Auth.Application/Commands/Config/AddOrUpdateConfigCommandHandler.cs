﻿using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Common;
using Feijuca.Auth.Common.Errors;
using Mattioli.Configurations.Models;
using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Models;
using Feijuca.Auth.Services;

using MediatR;

namespace Feijuca.Auth.Application.Commands.Config
{
    public class AddOrUpdateConfigCommandHandler(IConfigRepository configRepository, ITenantService tenantService) : IRequestHandler<AddOrUpdateConfigCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(AddOrUpdateConfigCommand request, CancellationToken cancellationToken)
        {
            bool result;

            tenantService.SetTenants([new Tenant(request!.Request!.Realms!.First()!.Name!)]);

            request.Request!.Realms!.First()!.DefaultSwaggerTokenGeneration = true;
            request.Request!.Realms!.First()!.Audience = Constants.FeijucaApiClientName;

            var existingConfig = await configRepository.GetConfigAsync();
            if (existingConfig == null)
            {
                result = await configRepository.AddConfigAsync(request.Request.ToEntity(), cancellationToken);

                if (result)
                {
                    return Result<bool>.Success(true);
                }

                return Result<bool>.Failure(ConfigErrors.InsertConfig);
            }
            else
            {
                var newRealm = new Models.Realm
                {
                    Audience = request.Request.Realms!.First().Audience,
                    DefaultSwaggerTokenGeneration = false,
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
}
