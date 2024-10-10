using Application.Responses;
using Common.Models;
using MediatR;

namespace Application.Queries.Clients
{
    public record GetAllClientsQuery(string Tenant) : IRequest<Result<IEnumerable<ClientResponse>>>;
}
