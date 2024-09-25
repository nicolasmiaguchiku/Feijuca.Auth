using Feijuca.Keycloak.MultiTenancy.Services;
using Microsoft.Extensions.DependencyInjection;
using TokenManager.Domain.Entities;
using TokenManager.Domain.Interfaces;
using TokenManager.Infra.Data.Repositories;

namespace TokenManager.Infra.CrossCutting.Extensions
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
