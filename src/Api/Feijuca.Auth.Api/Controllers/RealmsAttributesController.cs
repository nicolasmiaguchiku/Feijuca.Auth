using Feijuca.Auth.Application.Commands.RealmAttributes;
using Feijuca.Auth.Application.Commands.UserAttributes;
using Feijuca.Auth.Application.Requests.RealmAttributes;
using Feijuca.Auth.Attributes;
using LiteBus.Commands.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Feijuca.Auth.Api.Controllers;

[Route("api/v1/realms-attributes")]
[ApiController]
[Authorize]
public class RealmsAttributesController(ICommandMediator commandMediator) : ControllerBase
{
    /// <summary>
    /// Adds a new attribute to the specified Keycloak realm.
    /// </summary>
    /// <param name="addRealmAttributeRequest">The request object containing the necessary details to add attribute to the realm.</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken"/> that can be used to signal cancellation for the operation.</param>
    /// <returns>
    /// A 201 Created status code if the user is successfully created;
    /// otherwise, a 400 Bad Request status code with an error message.
    /// </returns>
    [HttpPost]
    [EndpointDescription("This endpoint add new attributes related to the an existing realm.")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [RequiredRole("Feijuca.ApiWriter")]
    public async Task<IActionResult> AddAtribute([FromBody] AddRealmAttributesRequest addRealmAttributeRequest,
        CancellationToken cancellationToken)
    {
        var result = await commandMediator.SendAsync(new AddRealmAttributesCommand(addRealmAttributeRequest), cancellationToken);

        if (result.IsSuccess)
        {
            return Created("/", true);
        }

        return BadRequest(result.Error);
    }

    /// <summary>
    /// Updates attributes for the specified Keycloak realm.
    /// </summary>
    /// <param name="updateRealmAttributeRequest">The request object containing the necessary details to update attributes for the realm.</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken"/> that can be used to signal cancellation for the operation.</param>
    /// <returns>
    /// A 204 No Content status code if the realm attributes are successfully updated;
    /// otherwise, a 400 Bad Request status code with an error message.
    /// </returns>
    [HttpPatch]
    [EndpointDescription("This endpoint update existing attributes related to the an existing realm.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [RequiredRole("Feijuca.ApiWriter")]
    public async Task<IActionResult> UpdateAttributes([FromBody] UpdateRealmAttributesRequest updateRealmAttributeRequest, CancellationToken cancellationToken)
    {
        var result = await commandMediator.SendAsync(new UpdateRealmAttributesCommand(updateRealmAttributeRequest), cancellationToken);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return BadRequest(result.Error);
    }

    /// <summary>
    /// Removes a existing attributes from a specified Keycloak realm.
    /// </summary>
    /// <param name="attributeKeys">The request object containing the necessary details to add attribute to the user.</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken"/> that can be used to signal cancellation for the operation.</param>
    /// <returns>
    /// A 204 No Content status code if the user is successfully created;
    /// otherwise, a 400 Bad Request status code with an error message.
    /// </returns>
    [HttpDelete]
    [EndpointDescription("This endpoint delete existing attributes related to the an existing realm.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [RequiredRole("Feijuca.ApiWriter")]
    public async Task<IActionResult> DeleteAttributes([FromBody] IEnumerable<string> attributeKeys, CancellationToken cancellationToken)
    {
        var result = await commandMediator.SendAsync(new DeleteRealmAttributesCommand(attributeKeys), cancellationToken);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return BadRequest(result.Error);
    }
}
