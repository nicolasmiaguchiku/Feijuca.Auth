﻿using Mattioli.Configurations.Models;
using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Extensions;
using Feijuca.Auth.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.IdentityModel.Tokens.Jwt;
using Feijuca.Auth.Common.Models;

namespace Feijuca.Auth.Infra.CrossCutting.Extensions;

public static class AuthExtension
{
    public static IServiceCollection AddApiAuthentication(this IServiceCollection services, out KeycloakSettings serverSettings)
    {
        var serviceProvider = services.BuildServiceProvider();
        var KeycloakSettingsRepository = serviceProvider.GetRequiredService<IConfigRepository>();
        serverSettings = KeycloakSettingsRepository.GetConfigAsync().GetAwaiter().GetResult();

        services.AddHttpContextAccessor();

        if (serverSettings is not null)
        {
            services.AddKeyCloakAuth(serverSettings.Realms ?? []);

            return services;
        }

        services.TryAddScoped<ITenantService, TenantService>();
        services.TryAddSingleton<JwtSecurityTokenHandler>();

        return services;
    }
}
