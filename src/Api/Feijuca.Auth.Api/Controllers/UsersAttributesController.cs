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
        /// <param name="username">The username necessary to get attributes from an user.</param>
        /// <param name="updateUserAttributeRequest">The request object containing the necessary details to add attribute to the user.</param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken"/> that can be used to signal cancellation for the operation.</param>
        /// <returns>
        /// A 201 Created status code if the user is successfully created;
        /// otherwise, a 400 Bad Request status code with an error message.
        /// </returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        public async Task<IActionResult> GetUserAttributes([FromRoute] string username, UpdateUserAttributeRequest updateUserAttributeRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateUserAttributesCommand(username, updateUserAttributeRequest), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Response);
            }

            return BadRequest(result.Error);
        }
    }
}
