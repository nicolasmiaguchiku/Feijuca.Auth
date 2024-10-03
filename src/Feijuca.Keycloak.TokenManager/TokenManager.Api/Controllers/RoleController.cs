using Feijuca.Keycloak.MultiTenancy.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenManager.Application.Commands.Role;
using TokenManager.Application.Queries.Permissions;
using TokenManager.Application.Requests.Role;
using TokenManager.Common.Models;

namespace TokenManager.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class RoleController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Get all roles available in all clients.
        /// </summary>
        [HttpGet]
        [Route("{tenant}/roles", Name = nameof(GetRoles))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiReader")]
        public async Task<IActionResult> GetRoles([FromRoute] string tenant, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetRolesQuery(tenant), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(Result<string>.Failure(result.Error));
        }

        /// <summary>
        /// Add a new role to a client.
        /// </summary>
        [HttpPost]
        [Route("{tenant}/role", Name = nameof(AddRole))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        public async Task<IActionResult> AddRole([FromRoute] string tenant, [FromBody] AddRoleRequest addRoleRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddRoleCommand(tenant, addRoleRequest), cancellationToken);

            if (result.IsSuccess)
            {
                return CreatedAtRoute(nameof(GetRoles), new { tenant }, result.Data); // Retorna 201 Created
            }

            return BadRequest(Result<string>.Failure(result.Error));
        }
    }
}
