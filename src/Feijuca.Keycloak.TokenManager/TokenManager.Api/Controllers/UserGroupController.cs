using Feijuca.Keycloak.MultiTenancy.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenManager.Application.Commands.UserGroup;
using TokenManager.Application.Queries.UserGroup;
using TokenManager.Common.Models;

namespace TokenManager.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class UserGroupController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Add a user to a specific group in the Keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpPost]
        [Route("addUserToGroup/{tenant}/{userId:guid}/{groupId:guid}", Name = nameof(AddUserToGroup))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        public async Task<IActionResult> AddUserToGroup([FromRoute] string tenant, [FromRoute] Guid userId, [FromRoute] Guid groupId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddUserToGroupCommand(tenant, userId, groupId), cancellationToken);

            if (result.IsSuccess)
            {
                return NoContent();
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }

        /// <summary>
        /// Get all users presents on a group in the Keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpGet]
        [Route("getUsersInGroup/{tenant}/{groupName}", Name = nameof(GetUsersInGroup))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiReader")]
        public async Task<IActionResult> GetUsersInGroup([FromRoute] string tenant, string groupName, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUsersGroupQuery(tenant, groupName), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }

        /// <summary>
        /// Add a user to a specific group in the Keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpDelete]
        [Route("removeUserFromGroup/{tenant}/{userId:guid}/{groupId:guid}", Name = nameof(RemoveUserFromGroup))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        public async Task<IActionResult> RemoveUserFromGroup([FromRoute] string tenant, [FromRoute] Guid userId, [FromRoute] Guid groupId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RemoveUserFromGroupCommand(tenant, userId, groupId), cancellationToken);

            if (result.IsSuccess)
            {
                return Accepted();
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }
    }
}
