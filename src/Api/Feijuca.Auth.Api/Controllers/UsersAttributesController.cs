using Feijuca.Auth.Application.Commands.UserAttributes;
using Feijuca.Auth.Application.Requests.UsersAttributes;
using Feijuca.Auth.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feijuca.Auth.Api.Controllers
{
    [Route("api/v1/users-attributes")]
    [ApiController]
    public class UsersAttributesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Adds a new attribute to a user on the specified Keycloak realm.
        /// </summary>
        /// <param name="userName">The username necessary to get an user.</param>
        /// <param name="addUserAttributeRequest">The request object containing the necessary details to add attribute to the user.</param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken"/> that can be used to signal cancellation for the operation.</param>
        /// <returns>
        /// A 201 Created status code if the user is successfully created;
        /// otherwise, a 400 Bad Request status code with an error message.
        /// </returns>
        /// <response code="201">The user was created successfully.</response>
        /// <response code="400">The request was invalid or could not be processed.</response>
        /// <response code="401">The request lacks valid authentication credentials.</response>
        /// <response code="403">The request was understood, but the server is refusing to fulfill it due to insufficient permissions.</response>
        /// <response code="500">An internal server error occurred while processing the request.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        public async Task<IActionResult> AddAtribute([FromRoute] string userName, AddUserAttributesRequest addUserAttributeRequest,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddUserAttributeCommand(userName, addUserAttributeRequest), cancellationToken);
            return BadRequest(result.Error);
        }

        /// <summary>
        /// Adds a new attribute to a user on the specified Keycloak realm.
        /// </summary>
        /// <param name="username">The username necessary to get an user.</param>
        /// <param name="addUserAttributesRequest">The request object containing the necessary details to add attribute to the user.</param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken"/> that can be used to signal cancellation for the operation.</param>
        /// <returns>
        /// A 201 Created status code if the user is successfully created;
        /// otherwise, a 400 Bad Request status code with an error message.
        /// </returns>
        /// <response code="201">The user was created successfully.</response>
        /// <response code="400">The request was invalid or could not be processed.</response>
        /// <response code="401">The request lacks valid authentication credentials.</response>
        /// <response code="403">The request was understood, but the server is refusing to fulfill it due to insufficient permissions.</response>
        /// <response code="500">An internal server error occurred while processing the request.</response>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        public async Task<IActionResult> GetUserAttributes([FromRoute] string username, [FromBody] AddUserAttributesRequest addUserAttributesRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddUserAttributeCommand(username, addUserAttributesRequest), cancellationToken);
            return BadRequest(result.Error);
        }
    }
}
