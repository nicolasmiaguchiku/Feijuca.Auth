using Feijuca.Keycloak.MultiTenancy.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenManager.Application.Commands.GroupRoles;
using TokenManager.Application.Queries.GroupRoles;
using TokenManager.Application.Requests.GroupRoles;
using TokenManager.Common.Models;

namespace TokenManager.Api.Controllers
{
    [Route("api/v1/auth/{tenant}/groups")]
    [ApiController]
    [Authorize]
    public class GroupRolesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Add a role to a specific group in the Keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpPost("{groupId}/roles", Name = nameof(AddRoleToGroup))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        public async Task<IActionResult> AddRoleToGroup([FromRoute] string tenant, [FromRoute] Guid groupId, [FromBody] RoleToGroupRequest addRoleToGroup, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddRoleToGroupCommand(tenant, groupId, addRoleToGroup), cancellationToken);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetGroupRoles), new { tenant, groupId }, null);
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }

        /// <summary>
        /// Remove a role from a specific group in the Keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpDelete]
        [Route("removeRoleFromGroup/{tenant}/{clientId}/{groupId}/{roleId}", Name = nameof(RemoveRoleFromGroup))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        public async Task<IActionResult> RemoveRoleFromGroup(
            [FromRoute] string tenant,
            [FromRoute] Guid clientId,
            [FromRoute] Guid groupId,
            [FromRoute] Guid roleId,
            CancellationToken cancellationToken)
        {
            var roleToGroupRequest = new RoleToGroupRequest(clientId, roleId);
            var result = await _mediator.Send(new RemoveRoleFromGroupCommand(tenant, groupId, roleToGroupRequest), cancellationToken);

            if (result.IsSuccess)
            {
                return NoContent();
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }

        /// <summary>
        /// Get roles associated with a group.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpGet("{groupId}/roles", Name = nameof(GetGroupRoles))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiReader")]
        public async Task<IActionResult> GetGroupRoles([FromRoute] string tenant, [FromRoute] Guid groupId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetGroupRolesQuery(tenant, groupId), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }
    }
}
