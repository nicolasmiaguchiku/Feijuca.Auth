using Feijuca.Auth.Application.Responses;
using Feijuca.Auth.Common.Models;

using MediatR;

namespace Feijuca.Auth.Application.Queries.Clients
{
    public record GetAllClientsQuery() : IRequest<Result<IEnumerable<ClientResponse>>>;
}
