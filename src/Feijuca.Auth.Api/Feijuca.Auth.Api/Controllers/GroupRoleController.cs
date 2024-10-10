using Application.Commands.GroupRoles;
using Application.Queries.GroupRoles;
using Application.Requests.GroupRoles;
using Common.Models;
using Feijuca.MultiTenancy.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class GroupRoleController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Add a role to a specific group in the Keycloak realm.
        /// </summary>
        [HttpPost("{tenant}/group/{id}/role", Name = nameof(AddRoleToGroup))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        public async Task<IActionResult> AddRoleToGroup(
            [FromRoute] string tenant,
            [FromRoute] Guid id,
            [FromBody] RoleToGroupRequest addRoleToGroup,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddRoleToGroupCommand(tenant, id, addRoleToGroup), cancellationToken);

            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetGroupRoles), new { tenant, id }, null);
            }

            return BadRequest(Result<string>.Failure(result.Error));
        }

        /// <summary>
        /// Remove a role from a specific group in the Keycloak realm.
        /// </summary>
        [HttpDelete("{tenant}/group/{groupid}/role/{roleid}", Name = nameof(RemoveRoleFromGroup))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        public async Task<IActionResult> RemoveRoleFromGroup(
            [FromRoute] string tenant,
            [FromRoute] Guid groupid,
            [FromRoute] Guid roleid,
            CancellationToken cancellationToken)
        {
            var roleToGroupRequest = new RoleToGroupRequest(groupid, roleid);
            var result = await _mediator.Send(new RemoveRoleFromGroupCommand(tenant, groupid, roleToGroupRequest), cancellationToken);

            if (result.IsSuccess)
            {
                return NoContent();
            }

            return BadRequest(Result<string>.Failure(result.Error));
        }

        /// <summary>
        /// Get roles associated with a group.
        /// </summary>
        [HttpGet("{tenant}/group/{id}/roles", Name = nameof(GetGroupRoles))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiReader")]
        public async Task<IActionResult> GetGroupRoles(
            [FromRoute] string tenant,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetGroupRolesQuery(tenant, id), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Response);
            }

            return BadRequest(Result<string>.Failure(result.Error));
        }
    }
}
