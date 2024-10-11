using Application.Queries.Clients;

using Common.Models;

using Feijuca.MultiTenancy.Attributes;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class ClientController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Get all clients existing on the Keycloak realm.
        /// </summary>
        /// <returns>A status code related to the operation.</returns>
        [HttpGet]
        [Route("{tenant}/clients", Name = nameof(GetClients))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequiredRole("Feijuca.ApiReader")]
        public async Task<IActionResult> GetClients([FromRoute] string tenant, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllClientsQuery(tenant), cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Response);
            }

            var responseError = Result<string>.Failure(result.Error);
            return BadRequest(responseError);
        }

    }
}
