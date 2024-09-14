using MediatR;
using TokenManager.Application.Responses;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Queries.Permissions
{
    public class GetRolesQueryHandler(IClientRepository clientRepository) : IRequestHandler<GetRolesQuery, Result<IEnumerable<RoleResponse>>>
    {
        private readonly IClientRepository _clientRepository = clientRepository;
        public async Task<Result<IEnumerable<RoleResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var results = await _clientRepository.GetClientsAsync(request.Tenant);
            throw new NotImplementedException();
        }
    }
}
