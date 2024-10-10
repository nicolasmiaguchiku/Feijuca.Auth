using Domain.Entities;
using Domain.Interfaces;
using Feijuca.Keycloak.MultiTenancy.Services;
using Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.CrossCutting.Extensions
{
    public static class RepositoriesExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupUsersRepository, UserGroupRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IGroupRolesRepository, GroupRolesRepository>();

            var serviceProvider = services.BuildServiceProvider();
            var authService = serviceProvider.GetRequiredService<IAuthService>();

            var tokenCredentials = new TokenCredentials()
            {
                Client_Secret = authService.GetClientSecret(),
                Client_Id = authService.GetClientId(),
                ServerUrl = authService.GetServerUrl()
            };

            services.AddSingleton(tokenCredentials);
            return services;
        }
    }
}
