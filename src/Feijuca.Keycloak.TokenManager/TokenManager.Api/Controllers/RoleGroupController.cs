using Feijuca.Keycloak.MultiTenancy.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenManager.Application.Commands.RoleGroup;
using TokenManager.Application.Requests.RoleGroup;
using TokenManager.Common.Models;

namespace TokenManager.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class RoleGroupController(IMediator mediator) : ControllerBase
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
        public async Task<IActionResult> AddRoleToGroup([FromRoute] string tenant, [FromBody] AddRoleToGroupRequest addRoleToGroup, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddRoleToGroupCommand(tenant, addRoleToGroup), cancellationToken);

            if (result.IsSuccess)
            {
                return Created();
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }
    }
}
