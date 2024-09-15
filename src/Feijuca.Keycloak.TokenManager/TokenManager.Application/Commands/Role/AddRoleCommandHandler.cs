using MediatR;
using TokenManager.Common.Errors;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Commands.Role
{
    public class AddRoleCommandHandler(IRoleRepository roleRepository) : IRequestHandler<AddRoleCommand, Result<bool>>
    {
        private readonly IRoleRepository _roleRepository = roleRepository;

        public async Task<Result<bool>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _roleRepository.AddRoleAsync(request.Tenant, request.AddRoleRequest.ClientId, request.AddRoleRequest.Name, request.AddRoleRequest.Description);
            if (result.IsSuccess)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(RoleErrors.AddRoleErrors);
        }
    }
}
