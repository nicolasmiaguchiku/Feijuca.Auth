using Feijuca.Keycloak.MultiTenancy.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokenManager.Application.Commands.Users;
using TokenManager.Application.Queries.Users;
using TokenManager.Application.Requests.User;
using TokenManager.Common.Models;

namespace TokenManager.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Get all users existing on the Keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpGet]
        [Route("{tenant}/users", Name = nameof(GetUsers))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        [RequiredRole("Feijuca.ApiReader")]
        public async Task<IActionResult> GetUsers([FromRoute] string tenant, [FromQuery] GetUsersRequest getUsersRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUsersQuery(tenant, getUsersRequest), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }

        /// <summary>
        /// Add a new user on the Keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpPost]
        [Route("{tenant}/users", Name = nameof(CreateUser))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        [Authorize]
        public async Task<IActionResult> CreateUser([FromRoute] string tenant, [FromBody] AddUserRequest addUserRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateUserCommand(tenant, addUserRequest), cancellationToken);

            if (result.IsSuccess)
            {
                var response = Result<string>.Success("User created successfully");
                return Created($"/api/v1/users/{tenant}", response.Data);
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError.Error);
        }

        /// <summary>
        /// Delete an existing user on the Keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpDelete]
        [Route("{tenant}/users/{id}", Name = nameof(DeleteUser))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromRoute] string tenant, [FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteUserCommand(tenant, id), cancellationToken);

            if (result.IsSuccess)
            {
                return NoContent();
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }
    }
}
