using MediatR;
using TokenManager.Application.Responses;
using TokenManager.Common.Models;

namespace TokenManager.Application.Queries.Clients
{
    public record GetAllClientsQuery(string Tenant) : IRequest<Result<IEnumerable<ClientResponse>>>;
}
