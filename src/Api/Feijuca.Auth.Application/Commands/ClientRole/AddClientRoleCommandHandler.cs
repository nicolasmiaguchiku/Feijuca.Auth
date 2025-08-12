using Feijuca.Auth.Common.Errors;
using Mattioli.Configurations.Models;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.ClientRole
{
    public class AddClientRoleCommandHandler(IClientRoleRepository clientRolesRepository) : IRequestHandler<AddClientRoleCommand, Result<bool>>
    {
        private readonly IClientRoleRepository _roleRepository = clientRolesRepository;

        public async Task<Result<bool>> Handle(AddClientRoleCommand request, CancellationToken cancellationToken)
        {
            foreach (var clientRole in request.AddClientRolesRequest)
            {
                var result = await _roleRepository.AddClientRoleAsync(clientRole.ClientId, clientRole.Name, clientRole.Description, cancellationToken);

                if (!result.IsSuccess)
                {
                    return Result<bool>.Failure(RoleErrors.AddRoleErrors);
                }
            }

            return Result<bool>.Success(true);            
        }
    }
}
