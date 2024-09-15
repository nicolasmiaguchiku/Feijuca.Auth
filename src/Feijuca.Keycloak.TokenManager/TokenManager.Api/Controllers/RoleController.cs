using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokenManager.Application.Commands.Role;
using TokenManager.Application.Queries.Permissions;
using TokenManager.Application.Requests.Role;
using TokenManager.Common.Models;

namespace TokenManager.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class RoleController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Get all roles available in all clients.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpGet]
        [Route("getRoles/{tenant}", Name = nameof(GetRoles))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRoles([FromRoute] string tenant, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetRolesQuery(tenant), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }

        /// <summary>
        /// Add a new role to a client.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpPost]
        [Route("addRole/{tenant}", Name = nameof(AddRole))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddRole([FromRoute] string tenant, [FromBody] AddRoleRequest addRoleRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddRoleCommand(tenant, addRoleRequest), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }
    }
}
