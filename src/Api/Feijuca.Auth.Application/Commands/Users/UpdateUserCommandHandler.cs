using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Users
{
    public class UpdateUserCommandHandler(IUserRepository _userRepository, ITenantService tenantService) : IRequestHandler<UpdateUserCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.UserRequest.ToDomain(tenantService.Tenant);
            var result = await _userRepository.UpdateUserAsync(request.Id, user, cancellationToken);
            return result;
        }
    }
}
