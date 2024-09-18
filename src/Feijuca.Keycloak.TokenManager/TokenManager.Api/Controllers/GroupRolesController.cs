using Feijuca.Keycloak.MultiTenancy.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenManager.Application.Commands.GroupRoles;
using TokenManager.Application.Queries.GroupRoles;
using TokenManager.Application.Requests.Group;
using TokenManager.Application.Requests.GroupRoles;
using TokenManager.Common.Models;

namespace TokenManager.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class GroupRolesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Add a role to a specific group in the Keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpPost]
        [Route("addRoleToGroup/{tenant}", Name = nameof(AddRoleToGroup))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        public async Task<IActionResult> AddRoleToGroup([FromRoute] string tenant, [FromBody] RoleToGroupRequest addRoleToGroup, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddRoleToGroupCommand(tenant, addRoleToGroup), cancellationToken);

            if (result.IsSuccess)
            {
                return Created();
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }

        /// <summary>
        /// Remove a role to a specific group in the Keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpDelete]
        [Route("removeRoleToGroup/{tenant}", Name = nameof(RemoveRoleToGroup))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveRoleToGroup([FromRoute] string tenant, [FromBody] RoleToGroupRequest removeRoleFromGroup, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RemoveRoleFromGroupCommand(tenant, removeRoleFromGroup), cancellationToken);

            if (result.IsSuccess)
            {
                return Created();
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }

        /// <summary>
        /// Get a roles associated to a group.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpGet]
        [Route("getGroupRoles/{tenant}", Name = nameof(GetGroupRoles))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiReader")]
        public async Task<IActionResult> GetGroupRoles([FromRoute] string tenant, [FromRoute] GetGroupRequest group, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetGroupRolesQuery(tenant, group.GroupId), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }
    }
}
