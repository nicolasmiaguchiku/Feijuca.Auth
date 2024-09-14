using MediatR;
using Microsoft.AspNetCore.Mvc;

using TokenManager.Application.Commands.UserGroup;
using TokenManager.Common.Models;

namespace TokenManager.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
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
    }
}
