using Feijuca.Auth.Application.Commands.Client;
using Feijuca.Auth.Application.Commands.ClientRole;
using Feijuca.Auth.Application.Commands.ClientScopeProtocol;
using Feijuca.Auth.Application.Commands.ClientScopes;
using Feijuca.Auth.Application.Commands.Config;
using Feijuca.Auth.Application.Commands.Group;
using Feijuca.Auth.Application.Commands.GroupRoles;
using Feijuca.Auth.Application.Commands.GroupUser;
using Feijuca.Auth.Application.Commands.Realm;
using Feijuca.Auth.Application.Commands.Users;
using Feijuca.Auth.Application.Queries.Clients;
using Feijuca.Auth.Application.Queries.ClientScopes;
using Feijuca.Auth.Application.Queries.Groups;
using Feijuca.Auth.Application.Queries.Permissions;
using Feijuca.Auth.Application.Requests.Client;
using Feijuca.Auth.Application.Requests.ClientScopes;
using Feijuca.Auth.Application.Requests.Config;
using Feijuca.Auth.Application.Requests.GroupRoles;
using Feijuca.Auth.Application.Requests.Realm;
using Feijuca.Auth.Application.Requests.Role;
using Feijuca.Auth.Application.Requests.User;
using Feijuca.Auth.Common;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;
using Flurl;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feijuca.Auth.Api.Controllers
{
    [Route("api/v1/configs")]
    [ApiController]
    public class ConfigsController(IMediator mediator, ITenantService tenantService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("existing-realm")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddClientConfigs([FromBody] AddKeycloakSettingsRequest addKeycloakSettings, CancellationToken cancellationToken)
        {
            return await AddOrUpdateClientConfigs(addKeycloakSettings, false, cancellationToken);
        }

        [HttpPost("new-realm")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddClientAndRealmConfigs([FromBody] AddKeycloakSettingsRequest addKeycloakSettings, CancellationToken cancellationToken)
        {
            return await AddOrUpdateClientConfigs(addKeycloakSettings, true, cancellationToken);
        }

        private async Task<IActionResult> AddOrUpdateClientConfigs(AddKeycloakSettingsRequest addKeycloakSettings, bool includeRealm, CancellationToken cancellationToken)
        {
            try
            {
                tenantService.SetTenant(addKeycloakSettings.Realm.Name!);

                // Configuração inicial
                PrepareRealmConfiguration(addKeycloakSettings);

                if (includeRealm)
                {
                    var addRealmRequest = new AddRealmRequest(addKeycloakSettings.Realm.Name!, "", addKeycloakSettings.Realm.DefaultSwaggerTokenGeneration);
                    var realmResult = await _mediator.Send(new AddRealmsCommand([addRealmRequest]), cancellationToken);
                    if (realmResult.IsFailure)
                    {
                        return BadRequest("Failed to create realm.");
                    }
                }

                var keyCloakSettings = CreateKeycloakSettings(addKeycloakSettings);

                // Adiciona configurações básicas
                var result = await ProcessBasicConfiguration(keyCloakSettings, cancellationToken);
                if (result.IsFailure)
                {
                    return BadRequest("Failed when tried added basic configurations.");
                }

                // Configurações adicionais
                result = await ProcessAdditionalConfiguration(cancellationToken);
                if (result.IsFailure)
                {
                    return BadRequest("Error while adding default configurations.");
                }

                // Configurações de usuários e grupos
                await ProcessUserAndGroupConfiguration(addKeycloakSettings, cancellationToken);

                return Created("/api/v1/config", "Initial configs created successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }

        private static void PrepareRealmConfiguration(AddKeycloakSettingsRequest addKeycloakSettings)
        {
            addKeycloakSettings.Realm.DefaultSwaggerTokenGeneration = true;
            addKeycloakSettings.Realm.Issuer = addKeycloakSettings.ServerSettings.Url
                .AppendPathSegment("realms")
                .AppendPathSegment(addKeycloakSettings.Realm.Name);
            addKeycloakSettings.Realm.Audience = Constants.FeijucaApiClientName;
        }

        private static KeycloakSettings CreateKeycloakSettings(AddKeycloakSettingsRequest addKeycloakSettings)
        {
            return new KeycloakSettings
            {
                Client = addKeycloakSettings.MasterClient,
                Secrets = addKeycloakSettings.MasterClientSecret,
                ServerSettings = addKeycloakSettings.ServerSettings,
                Realms = new[] { addKeycloakSettings.Realm }
            };
        }

        private async Task<Result> ProcessBasicConfiguration(KeycloakSettings keyCloakSettings, CancellationToken cancellationToken)
        {
            var clientBody = new AddClientRequest
            {
                ClientId = keyCloakSettings.Client.ClientId,
                Description = "This client is related to Feijuca.Api, this client will handle token generation and keycloak actions.",
                Urls = [$"{Request.Scheme}://{Request.Host}", $"{Request.Scheme}s://{Request.Host}"]
            };

            var addClientScopes = new List<AddClientScopesRequest>
            {
                new(Constants.FeijucaApiClientName, Constants.FeijucaApiClientName, true)
            };

            return await ProcessActionsAsync(
                async () => await _mediator.Send(new AddConfigCommand(keyCloakSettings), cancellationToken),
                async () => await _mediator.Send(new AddClientCommand(clientBody), cancellationToken),
                async () => await _mediator.Send(new AddClientScopesCommand(addClientScopes), cancellationToken));
        }

        private async Task<Result> ProcessAdditionalConfiguration(CancellationToken cancellationToken)
        {
            var clientScopes = await _mediator.Send(new GetClientScopesQuery(), cancellationToken);
            var clients = await _mediator.Send(new GetAllClientsQuery(), cancellationToken);
            var clientScope = clientScopes.FirstOrDefault(x => x.Name == Constants.FeijucaApiClientName)!;
            var feijucaClient = clients.FirstOrDefault(x => x.ClientId == Constants.FeijucaApiClientName)!;

            var addClientScopeToClientRequest = new AddClientScopeToClientRequest(feijucaClient.Id, clientScope.Id, false);
            var groupRequest = new AddGroupRequest(Constants.FeijucaGroupName, []);

            var addRolesRequest = new List<AddClientRoleRequest>
            {
                new(feijucaClient.Id, Constants.FeijucaRoleReadName, "Role related to the action to read data on the realm."),
                new(feijucaClient.Id, Constants.FeijucaRoleWriterName, "Role related to the action to write data on the realm.")
            };

            return await ProcessActionsAsync(
                async () => await _mediator.Send(new AddClientScopeToClientCommand(addClientScopeToClientRequest), cancellationToken),
                async () => await _mediator.Send(new AddGroupCommand(groupRequest), cancellationToken),
                async () => await _mediator.Send(new AddClientRoleCommand(addRolesRequest), cancellationToken),
                async () => await _mediator.Send(new AddClientScopeAudienceProtocolMapperCommand(clientScope.Id), cancellationToken));
        }

        private async Task ProcessUserAndGroupConfiguration(AddKeycloakSettingsRequest addKeycloakSettings, CancellationToken cancellationToken)
        {
            var clients = await _mediator.Send(new GetAllClientsQuery(), cancellationToken);
            var feijucaClient = clients.FirstOrDefault(x => x.ClientId == Constants.FeijucaApiClientName)!;

            var clientRoles = await _mediator.Send(new GetClientRolesQuery(), cancellationToken);
            var groups = await _mediator.Send(new GetAllGroupsQuery(), cancellationToken);

            var feijucaGroup = groups.Response.FirstOrDefault(x => x.Name == Constants.FeijucaGroupName);
            var feijucaRoles = clientRoles.Response.FirstOrDefault(x => x.Id == feijucaClient.Id)!.Roles;

            foreach (var roleName in new[] { Constants.FeijucaRoleReadName, Constants.FeijucaRoleWriterName })
            {
                var roleId = feijucaRoles.First(x => x.Name == roleName).Id;
                var clientRole = new AddClientRoleToGroupRequest(feijucaClient.Id, roleId);
                await _mediator.Send(new AddClientRoleToGroupCommand(feijucaGroup!.Id.ToString(), clientRole), cancellationToken);
            }

            var addUserRequest = new AddUserRequest(
                addKeycloakSettings.RealmAdminUser.Email,
                addKeycloakSettings.RealmAdminUser.Password,
                addKeycloakSettings.RealmAdminUser.Email,
                string.Empty,
                string.Empty,
                []);

            var userId = await _mediator.Send(new AddUserCommand(addUserRequest), cancellationToken);

            await _mediator.Send(new AddUserToGroupCommand(userId.Response, Guid.Parse(feijucaGroup!.Id)), cancellationToken);
        }


        private static async Task<Result> ProcessActionsAsync(params Func<Task<Result>>[] actions)
        {
            foreach (var action in actions)
            {
                var result = await action();
                if (result.IsFailure)
                {
                    return result;
                }
            }
            return Result.Success();
        }
    }
}
