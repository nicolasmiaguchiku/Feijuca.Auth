using MediatR;

using TokenManager.Application.Mappers;
using TokenManager.Common.Errors;
using TokenManager.Common.Models;
using TokenManager.Domain.Entities;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Commands.Users
{
    public class CreateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            AddTenantToRequest(request);
            var user = request.AddUserRequest.ToDomain();

            var (IsSuccessStatusCode, contentRequest) = await _userRepository.CreateAsync(request.Tenant, user);
            if (IsSuccessStatusCode)
            {
                await SetUserPasswordAsync(request.Tenant, user);
                return Result.Success();
            }

            UserErrors.SetTechnicalMessage(contentRequest);
            return Result.Failure(UserErrors.WrongPasswordDefinition);
        }

        private static void AddTenantToRequest(CreateUserCommand request)
        {
            request.AddUserRequest.Attributes.Add("Tenant", request.Tenant);
        }

        private async Task SetUserPasswordAsync(string tenant, User user)
        {
            var keycloakUser = await _userRepository.GetAsync(tenant, user.Username);
            await _userRepository.ResetPasswordAsync(tenant, keycloakUser.Data.Id!, user.Password);
        }
    }
}
