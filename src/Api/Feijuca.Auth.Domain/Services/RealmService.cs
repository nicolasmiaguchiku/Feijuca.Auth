﻿using Feijuca.Auth.Common.Errors;
using Mattioli.Configurations.Models;
using Feijuca.Auth.Domain.Entities;
using Feijuca.Auth.Domain.Interfaces;
using Flurl;

namespace Feijuca.Auth.Domain.Services
{
    public class RealmService(IRealmRepository realmRepository, IConfigRepository configRepository) : IRealmService
    {
        public async Task<Result<bool>> AddNewRealmAsync(RealmEntity realm, CancellationToken cancellationToken)
        {
            var result = await realmRepository.CreateRealmAsync(realm, cancellationToken);

            if (!result)
            {
                return Result<bool>.Failure(RealmErrors.CreateRealmError);
            }

            var config = await configRepository.GetConfigAsync();

            if (!config.Realms?.Any(x => x.Name == realm.Realm) ?? false)
            {
                var realmsList = config.Realms?.ToList() ?? [];

                realmsList.Add(new Models.Realm
                {
                    Name = realm.Realm,
                    Audience = realm.Realm,
                    DefaultSwaggerTokenGeneration = realm.DefaultSwaggerTokenGeneration
                });

                config.Realms = realmsList;

                await configRepository.UpdateRealmConfigAsync(config.Id, config);
            }

            return Result<bool>.Success(true);
        }
    }
}
