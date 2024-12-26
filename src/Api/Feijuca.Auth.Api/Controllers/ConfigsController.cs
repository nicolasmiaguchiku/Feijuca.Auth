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
using Feijuca.Auth.Application.Queries.Config;
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
using Feijuca.Auth.Models;

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

        /// <summary>
        /// Retrieves the existing configuration settings.
        /// </summary>
        /// <returns>
        /// A 200 OK status code along with the configuration if the operation is successful;
        /// otherwise, a 400 Bad Request status code with an error message, or a 500 Internal Server Error status code if something goes wrong.
        /// </returns>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken"/> used to observe cancellation requests for the operation.</param>
        /// <response code="200">The operation was successful, and the configuration settings are returned.</response>
        /// <response code="400">The request was invalid or could not be processed.</response>
        /// <response code="500">An internal server error occurred during the processing of the request.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetConfig(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetConfigQuery(), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Response);
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError.Error);
        }

        /// <summary>
        /// Add a new client and client scopes related to the Feijuca.Auth.Api (Use when you have a realm created).
        /// </summary>
        /// <returns>
        /// A 201 Created status code along with the newly inserted configuration if the operation is successful;
        /// otherwise, a 400 Bad Request status code with an error message, or a 500 Internal Server Error status code if something goes wrong.
        /// </returns>
        /// <param name="addKeycloakSettings">An object of type <see cref="T:Feijuca.Auth.Common.Models.KeycloakSettings"/> containing the configuration details to be inserted.</param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken"/> used to observe cancellation requests for the operation.</param>
        /// <response code="201">The configuration was successfully inserted.</response>
        /// <response code="400">The request was invalid or could not be processed.</response>
        /// <response code="500">An internal server error occurred during the processing of the request.</response>
        [HttpPost("config-existing-realm")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddClientConfigs([FromBody] AddKeycloakSettingsRequest addKeycloakSettings, CancellationToken cancellationToken)
        {
            #region Add Feijuca.Auth config, Client and Client Scope 
            tenantService.SetTenant(addKeycloakSettings.Realm.Name!);

            addKeycloakSettings.Realm.DefaultSwaggerTokenGeneration = true;
            addKeycloakSettings.Realm.Issuer = addKeycloakSettings.ServerSettings.Url
                .AppendPathSegment("realms")
                .AppendPathSegment(addKeycloakSettings.Realm.Name);
            addKeycloakSettings.Realm.Audience = Constants.FeijucaApiClientName;

            var keyCloakSettings = new KeycloakSettings
            {
                Client = addKeycloakSettings.MasterClient,
                Secrets = addKeycloakSettings.MasterClientSecret,
                ServerSettings = addKeycloakSettings.ServerSettings,
                Realms = [addKeycloakSettings.Realm]
            };

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

            var result = await ProcessActionsAsync(
                async () => await _mediator.Send(new AddConfigCommand(keyCloakSettings), cancellationToken),
                async () => await _mediator.Send(new AddClientCommand(clientBody), cancellationToken),
                async () => await _mediator.Send(new AddClientScopesCommand(addClientScopes), cancellationToken));

            if (result.IsFailure)
            {
                return BadRequest("Failed when tried added basic configurations");
            }
            #endregion

            #region Add Client Scope to Client, Group and Client Role
            var clientScopes = await _mediator.Send(new GetClientScopesQuery(), cancellationToken);
            var clients = await _mediator.Send(new GetAllClientsQuery(), cancellationToken);
            var clientScope = clientScopes.FirstOrDefault(x => x.Name == Constants.FeijucaApiClientName)!;
            var feijucaClient = clients.FirstOrDefault(x => x.ClientId == Constants.FeijucaApiClientName)!;

            var addClientScopeToClientRequest = new AddClientScopeToClientRequest(feijucaClient.Id, clientScope.Id, false);

            var groupRequest = new AddGroupRequest(Constants.FeijucaGroupName, []);

            var addRolesRequest = new List<AddClientRoleRequest>()
            {
                new(feijucaClient.Id, Constants.FeijucaRoleReadName, "Role related to the action to read data on the realm."),
                new(feijucaClient.Id, Constants.FeijucaRoleWriterName, "Role related to the action to write data on the realm."),
            };

            result = await ProcessActionsAsync(
                async () => await _mediator.Send(new AddClientScopeToClientCommand(addClientScopeToClientRequest), cancellationToken),
                async () => await _mediator.Send(new AddGroupCommand(groupRequest), cancellationToken),
                async () => await _mediator.Send(new AddClientRoleCommand(addRolesRequest), cancellationToken),
                async () => await _mediator.Send(new AddClientScopeAudienceProtocolMapperCommand(clientScope.Id), cancellationToken));

            if (result.IsFailure)
            {
                return BadRequest("Error while tried added default configurations to the client Feijuca.Auth.Api.");
            }

            #endregion

            #region Add Client Role to Group, Add new User and Add User to Group
            var clientRoles = await _mediator.Send(new GetClientRolesQuery(), cancellationToken);
            var groups = await _mediator.Send(new GetAllGroupsQuery(), cancellationToken);

            var feijucaGroup = groups.Response.FirstOrDefault(x => x.Name == Constants.FeijucaGroupName);
            var feijucaRoles = clientRoles.Response.FirstOrDefault(x => x.Id == feijucaClient.Id)!.Roles;

            var clientRole = new AddClientRoleToGroupRequest(feijucaClient.Id, feijucaRoles.FirstOrDefault(x => x.Name == Constants.FeijucaRoleReadName)!.Id);
            await _mediator.Send(new AddClientRoleToGroupCommand(feijucaGroup!.Id.ToString(), clientRole), cancellationToken);

            clientRole = new AddClientRoleToGroupRequest(feijucaClient.Id, feijucaRoles.FirstOrDefault(x => x.Name == Constants.FeijucaRoleWriterName)!.Id);
            await _mediator.Send(new AddClientRoleToGroupCommand(feijucaGroup!.Id.ToString(), clientRole), cancellationToken);

            var addUserRequest = new AddUserRequest(addKeycloakSettings.RealmAdminUser.Email,
                    addKeycloakSettings.RealmAdminUser.Password,
                    addKeycloakSettings.RealmAdminUser.Email,
                    "",
                    "",
                    []);

            var userId = await _mediator.Send(new AddUserCommand(addUserRequest), cancellationToken);

            await _mediator.Send(new AddUserToGroupCommand(userId.Response, Guid.Parse(feijucaGroup.Id)), cancellationToken);

            #endregion

            return Created("/api/v1/config", "Initial configs created with succesfully!!!");
        }

        /// <summary>
        /// Add a new Keycloak realm, client and client scopes related to the Feijuca.Auth.Api (Use when you do not have a realm created).
        /// </summary>
        /// <returns>
        /// A 201 Created status code along with the newly inserted configuration if the operation is successful;
        /// otherwise, a 400 Bad Request status code with an error message, or a 500 Internal Server Error status code if something goes wrong.
        /// </returns>
        /// <param name="addKeycloakSettings">An object of type <see cref="T:Feijuca.Auth.Common.Models.KeycloakSettings"/> containing the configuration details to be inserted.</param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken"/> used to observe cancellation requests for the operation.</param>
        /// <response code="201">The configuration was successfully inserted.</response>
        /// <response code="400">The request was invalid or could not be processed.</response>
        /// <response code="500">An internal server error occurred during the processing of the request.</response>
        [HttpPost("config-new-realm")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAllConfigs([FromBody] AddKeycloakSettingsRequest addKeycloakSettings, CancellationToken cancellationToken)
        {
            #region Add Feijuca.Auth config, Realm, Client and Client Scope 
            tenantService.SetTenant(addKeycloakSettings.Realm.Name!);
            addKeycloakSettings.Realm.DefaultSwaggerTokenGeneration = true;
            addKeycloakSettings.Realm.Issuer = addKeycloakSettings.ServerSettings.Url
                .AppendPathSegment("realms")
                .AppendPathSegment(addKeycloakSettings.Realm.Name);
            addKeycloakSettings.Realm.Audience = Constants.FeijucaApiClientName;

            var keyCloakSettings = new KeycloakSettings
            {
                Client = addKeycloakSettings.MasterClient,
                Secrets = addKeycloakSettings.MasterClientSecret,
                ServerSettings = addKeycloakSettings.ServerSettings,
                Realms = [addKeycloakSettings.Realm]
            };

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

            var realms = new List<AddRealmRequest>
            {
                new(addKeycloakSettings.Realm.Name!, "", addKeycloakSettings.Realm.DefaultSwaggerTokenGeneration)
            };

            var result = await ProcessActionsAsync(
                async () => await _mediator.Send(new AddConfigCommand(keyCloakSettings), cancellationToken),
                async () => await _mediator.Send(new AddRealmsCommand(realms), cancellationToken),                
                async () => await _mediator.Send(new AddClientCommand(clientBody), cancellationToken),
                async () => await _mediator.Send(new AddClientScopesCommand(addClientScopes), cancellationToken));

            if (result.IsFailure)
            {
                return BadRequest("Failed when tried added basic configurations");
            }
            #endregion

            #region Add Client Scope to Client, Group and Client Role
            var clientScopes = await _mediator.Send(new GetClientScopesQuery(), cancellationToken);
            var clients = await _mediator.Send(new GetAllClientsQuery(), cancellationToken);
            var feijucaClientScope = clientScopes.FirstOrDefault(x => x.Name == Constants.FeijucaApiClientName)!;
            var feijucaClient = clients.FirstOrDefault(x => x.ClientId == Constants.FeijucaApiClientName)!;

            var addClientScopeToClientRequest = new AddClientScopeToClientRequest(feijucaClient.Id, feijucaClientScope.Id, false);

            var groupRequest = new AddGroupRequest(Constants.FeijucaGroupName, []);

            var addRolesRequest = new List<AddClientRoleRequest>()
                {
                    new(feijucaClient.Id, Constants.FeijucaRoleReadName, "Role related to the action to read data on the realm."),
                    new(feijucaClient.Id, Constants.FeijucaRoleWriterName, "Role related to the action to write data on the realm."),
                };

            result = await ProcessActionsAsync(
                async () => await _mediator.Send(new AddClientScopeToClientCommand(addClientScopeToClientRequest), cancellationToken),
                async () => await _mediator.Send(new AddGroupCommand(groupRequest), cancellationToken),
                async () => await _mediator.Send(new AddClientRoleCommand(addRolesRequest), cancellationToken),
                async () => await _mediator.Send(new AddClientScopeAudienceProtocolMapperCommand(feijucaClientScope.Id), cancellationToken));

            if (result.IsFailure)
            {
                return BadRequest("Error while tried added default configurations to the client Feijuca.Auth.Api.");
            }

            #endregion

            #region Add Client Role to Group, Add new User and Add User to Group
            var clientRoles = await _mediator.Send(new GetClientRolesQuery(), cancellationToken);
            var groups = await _mediator.Send(new GetAllGroupsQuery(), cancellationToken);

            var feijucaGroup = groups.Response.FirstOrDefault(x => x.Name == Constants.FeijucaGroupName);
            var feijucaRoles = clientRoles.Response.FirstOrDefault(x => x.Id == feijucaClient.Id)!.Roles;

            var clientRole = new AddClientRoleToGroupRequest(feijucaClient.Id, feijucaRoles.FirstOrDefault(x => x.Name == Constants.FeijucaRoleReadName)!.Id);
            await _mediator.Send(new AddClientRoleToGroupCommand(feijucaGroup!.Id.ToString(), clientRole), cancellationToken);

            clientRole = new AddClientRoleToGroupRequest(feijucaClient.Id, feijucaRoles.FirstOrDefault(x => x.Name == Constants.FeijucaRoleWriterName)!.Id);
            await _mediator.Send(new AddClientRoleToGroupCommand(feijucaGroup!.Id.ToString(), clientRole), cancellationToken);

            var addUserRequest = new AddUserRequest(addKeycloakSettings.RealmAdminUser.Email,
                    addKeycloakSettings.RealmAdminUser.Password,
                    addKeycloakSettings.RealmAdminUser.Email,
                    "",
                    "",
                    []);

            var addUserResponse = await _mediator.Send(new AddUserCommand(addUserRequest), cancellationToken);

            await _mediator.Send(new AddUserToGroupCommand(addUserResponse.Response, Guid.Parse(feijucaGroup.Id)), cancellationToken);

            #endregion

            return Created("/api/v1/config", "Initial configs created with succesfully!!!");
        }


        private static async Task<Result> ProcessActionsAsync(params Func<Task<Result<bool>>>[] actions)
        {
            foreach (var action in actions)
            {
                var result = await action();
                if (!result.IsSuccess)
                {
                    return Result.Failure(result.Error);
                }
            }

            return Result.Success();
        }
    }
}
