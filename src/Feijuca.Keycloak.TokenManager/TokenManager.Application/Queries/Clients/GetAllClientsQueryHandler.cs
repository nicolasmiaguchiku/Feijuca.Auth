using MediatR;
using TokenManager.Application.Responses;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Queries.Clients
{
    public class GetAllClientsQueryHandler(IClientRepository clientRepository) : IRequestHandler<GetAllClientsQuery, Result<IEnumerable<ClientResponse>>>
    {
        private readonly IClientRepository _clientRepository = clientRepository;

        public async Task<Result<IEnumerable<ClientResponse>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clientsResult = await _clientRepository.GetClientsAsync(request.Tenant);
            
            if (clientsResult.IsSuccess)
            {
                var clients = clientsResult.Data.Select(x => new ClientResponse(x.Enabled, x.Id, x.ClientId));
                return Result<IEnumerable<ClientResponse>>.Success(clients);
            }

            return Result<IEnumerable<ClientResponse>>.Failure(clientsResult.Error);
        }
    }
}
