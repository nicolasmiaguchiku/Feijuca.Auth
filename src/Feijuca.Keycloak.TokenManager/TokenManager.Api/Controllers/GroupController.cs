using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokenManager.Application.Services.Commands.Users;
using TokenManager.Application.Services.Requests.User;
using TokenManager.Application.Services.Responses;

namespace TokenManager.Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class GroupController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Add a new group on the keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpPost]
        [Route("createGroup/{tenant}", Name = nameof(CreateGroup))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateGroup([FromRoute] string tenant, [FromBody] AddGroupRequest addGroupRequest, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateGroupCommand(tenant, addGroupRequest), cancellationToken);
            
            if (result.IsSuccess)
            {
                var response = ResponseResult<string>.Success("Group created successfully");
                return Created("/createUser", response);
            }

            var responseError = ResponseResult<string>.Failure(result.Error);
            return BadRequest(responseError);
        }
    }
}
