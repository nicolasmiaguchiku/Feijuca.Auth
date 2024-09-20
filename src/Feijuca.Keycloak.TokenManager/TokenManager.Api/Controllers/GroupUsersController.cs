using Feijuca.Keycloak.MultiTenancy.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenManager.Application.Commands.GroupUser;
using TokenManager.Application.Queries.GroupUser;
using TokenManager.Application.Requests.GroupUsers;
using TokenManager.Common.Models;

namespace TokenManager.Api.Controllers
{
    [Route("api/v1/auth/{tenant}/groups")]
    [ApiController]
    [Authorize]
    public class GroupUsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Add a user to a specific group in the Keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpPost("{groupId}/users", Name = nameof(AddUserToGroup))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        public async Task<IActionResult> AddUserToGroup([FromRoute] string tenant, [FromRoute] Guid groupId, [FromBody] AddUserToGroupRequest addUserToGroupRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddUserToGroupCommand(tenant, addUserToGroupRequest.UserId, groupId), cancellationToken);

            if (result.IsSuccess)
            {
                return NoContent();
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }

        /// <summary>
        /// Get all users present in a specific group in the Keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpGet("{groupId}/users", Name = nameof(GetUsersInGroup))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiReader")]
        public async Task<IActionResult> GetUsersInGroup([FromRoute] string tenant, [FromRoute] Guid groupId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUsersGroupQuery(tenant, groupId), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }

        /// <summary>
        /// Remove a user from a specific group in the Keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpDelete("{groupId}/users", Name = nameof(RemoveUserFromGroup))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        public async Task<IActionResult> RemoveUserFromGroup([FromRoute] string tenant, [FromRoute] Guid groupId, [FromBody] RemoveUserFromGroupRequest removeUserFromGroup, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RemoveUserFromGroupCommand(tenant, removeUserFromGroup.UserId, groupId), cancellationToken);

            if (result.IsSuccess)
            {
                return NoContent();
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }
    }
}
