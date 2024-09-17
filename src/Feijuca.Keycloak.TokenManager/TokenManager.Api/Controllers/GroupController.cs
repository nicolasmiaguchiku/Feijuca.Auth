using Feijuca.Keycloak.MultiTenancy.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenManager.Application.Commands.Group;
using TokenManager.Application.Queries.Groups;
using TokenManager.Application.Requests.RoleGroup;
using TokenManager.Application.Requests.User;
using TokenManager.Common.Models;

namespace TokenManager.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class GroupController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Get all groups existing on the keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpGet]
        [Route("getGroups/{tenant}", Name = nameof(GetGroups))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiReader")]
        public async Task<IActionResult> GetGroups([FromRoute] string tenant, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllGroupsQuery(tenant), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }

        /// <summary>
        /// Delete an existing group on the keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpDelete]
        [Route("deleteGroup/{tenant}/{id:guid}", Name = nameof(DeleteGroup))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        public async Task<IActionResult> DeleteGroup([FromRoute] string tenant, [FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteGroupCommand(tenant, id), cancellationToken);

            if (result.IsSuccess)
            {
                return NoContent();
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }

        /// <summary>
        /// Add a new group on the keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpPost]
        [Route("createGroup/{tenant}", Name = nameof(CreateGroup))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        public async Task<IActionResult> CreateGroup([FromRoute] string tenant, [FromBody] AddGroupRequest addGroupRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateGroupCommand(tenant, addGroupRequest), cancellationToken);
            
            if (result.IsSuccess)
            {
                var response = Result<string>.Success("Group created successfully");
                return Created("/createGroup", response);
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }
    }
}
