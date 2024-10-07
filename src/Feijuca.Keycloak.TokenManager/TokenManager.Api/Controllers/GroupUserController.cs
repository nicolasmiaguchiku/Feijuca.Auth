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
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class GroupUserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Add a user to a specific group in the Keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpPost("{tenant}/group/user", Name = nameof(AddUserToGroup))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        public async Task<IActionResult> AddUserToGroup([FromRoute] string tenant, 
            [FromBody] AddUserToGroupRequest addUserToGroupRequest, 
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddUserToGroupCommand(tenant, addUserToGroupRequest.UserId, addUserToGroupRequest.GroupId), cancellationToken);

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
        [HttpGet("{tenant}/group/user", Name = nameof(GetUsersInGroup))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiReader")]
        public async Task<IActionResult> GetUsersInGroup([FromRoute] string tenant, [FromQuery] GetUsersGroupRequest usersGroup, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUsersGroupQuery(tenant, usersGroup), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Response);
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }

        /// <summary>
        /// Remove a user from a specific group in the Keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpDelete("{tenant}/group/user", Name = nameof(RemoveUserFromGroup))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        public async Task<IActionResult> RemoveUserFromGroup([FromRoute] string tenant, 
            [FromBody] RemoveUserFromGroupRequest removeUserFromGroup, 
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RemoveUserFromGroupCommand(tenant, removeUserFromGroup.UserId, removeUserFromGroup.GroupId), cancellationToken);

            if (result.IsSuccess)
            {
                return NoContent();
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }
    }
}
