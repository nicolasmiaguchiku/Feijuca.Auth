using MediatR;
using TokenManager.Application.Mappers;
using TokenManager.Common.Models;
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
            var result = await _userRepository.CreateAsync(request.Tenant, user);

            if (result.IsSuccess)
            {
                var keycloakUser = await _userRepository.GetAsync(request.Tenant, user.Username);
                result = await _userRepository.ResetPasswordAsync(request.Tenant, keycloakUser.Response.Id, user.Password);

                if (result.IsSuccess)
                {
                    return result;
                }
            }

            return Result.Failure(result.Error);
        }

        private static void AddTenantToRequest(CreateUserCommand request)
        {
            request.AddUserRequest.Attributes.Add("Tenant", [request.Tenant]);
        }
    }
}
