using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.User
{
    public class AddUserCommandHandler(IUserRepository userRepository, ITenantService tenantService) : IRequestHandler<AddUserCommand, Result<Guid>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<Guid>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.AddUserRequest.ToDomain(tenantService.Tenant);
            var result = await _userRepository.CreateAsync(user, cancellationToken);

            if (result.IsSuccess)
            {
                var keycloakUser = await _userRepository.GetAsync(user.Username, cancellationToken);
                result = await _userRepository.ResetPasswordAsync(keycloakUser.Response.Id, user.Password, cancellationToken);

                if (result.IsSuccess)
                {
                    return Result<Guid>.Success(keycloakUser.Response.Id);
                }
            }

            return Result<Guid>.Failure(result.Error);
        }
    }
}
