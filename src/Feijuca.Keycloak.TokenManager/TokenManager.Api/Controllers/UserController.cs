using Feijuca.Keycloak.MultiTenancy.Attributes;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TokenManager.Application.Commands.Users;
using TokenManager.Application.Queries.Users;
using TokenManager.Application.Requests.User;
using TokenManager.Application.Responses;
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
        [Route("getUsers/{tenant}", Name = nameof(GetUsers))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        [RequiredRole("Feijuca.ApiReader")]
        public async Task<IActionResult> GetUsers([FromRoute] string tenant, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllUsersQuery(tenant), cancellationToken);

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
        [Route("createUser/{tenant}", Name = nameof(Create))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        [Authorize]
        public async Task<IActionResult> Create([FromRoute] string tenant, [FromBody] AddUserRequest addUserRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateUserCommand(tenant, addUserRequest), cancellationToken);

            if (result.IsSuccess)
            {
                var response = Result<string>.Success("User created successfully");
                return Created("/createUser", response.Data);
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError.Error);
        }

        /// <summary>
        /// DeleteGroup an existing user on the Keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpDelete]
        [Route("deleteUser/{tenant}", Name = nameof(Delete))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] string tenant, [FromBody] DeleteUserRequest deleteUserRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteUserCommand(tenant, deleteUserRequest.UserId), cancellationToken);

            if (result.IsSuccess)
            {
                return NoContent();
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }

        /// <summary>
        /// Return a valid JWT token and details about them.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpPost]
        [Route("login/{tenant}", Name = nameof(Login))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromRoute] string tenant, [FromBody] LoginUserRequest loginUserRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new LoginUserCommand(tenant, loginUserRequest), cancellationToken);

            if (result.IsSuccess)
            {
                var response = Result<TokenDetailsResponse>.Success(result.Data);
                return Ok(response.Data);
            }

            var responseError = Result<TokenDetailsResponse>.Failure(result.Error);
            return BadRequest(responseError);
        }

        /// <summary>
        /// Return a valid JWT token and details about them refreshed.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpPost]
        [Route("refreshToken/{tenant}", Name = nameof(RefreshToken))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiWriter")]
        [Authorize]
        public async Task<IActionResult> RefreshToken([FromRoute] string tenant, [FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RefreshTokenCommand(tenant, request.RefreshToken), cancellationToken);

            if (result.IsSuccess)
            {
                var response = Result<TokenDetailsResponse>.Success(result.Data);
                return Ok(response);
            }

            var responseError = Result<TokenDetailsResponse>.Failure(result.Error);
            return BadRequest(responseError);
        }
    }
}
