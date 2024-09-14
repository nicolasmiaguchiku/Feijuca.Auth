using MediatR;

using TokenManager.Application.Mappers;
using TokenManager.Application.Services.Commands.Users;
using TokenManager.Common.Models;
using TokenManager.Domain.Entities;
using TokenManager.Domain.Errors;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Commands.Users
{
    public class CreateUserCommandHandler(IUserRepository userRepository, ITokenRepository tokenRepository) : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenRepository _tokenRepository = tokenRepository;

        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            AddTenantToRequest(request);
            var accessTokenResult = await _tokenRepository.GetAccessTokenAsync(request.Tenant);
            if (accessTokenResult.IsSuccess)
            {
                var accessToken = accessTokenResult.Data.Access_Token;
                var user = request.AddUserRequest.ToDomain();

                var (IsSuccessStatusCode, contentRequest) = await _userRepository.CreateNewUserAsync(request.Tenant, user, accessToken);
                if (IsSuccessStatusCode)
                {
                    await SetUserPasswordAsync(request.Tenant, user, accessToken);
                    return Result.Success();
                }

                UserErrors.SetTechnicalMessage(contentRequest);
                return Result.Failure(UserErrors.WrongPasswordDefinition);
            }

            return Result.Failure(UserErrors.TokenGenerationError);
        }

        private static void AddTenantToRequest(CreateUserCommand request)
        {
            request.AddUserRequest.Attributes.Add("tenant", request.Tenant);
        }

        private async Task SetUserPasswordAsync(string tenant, User user, string accessToken)
        {
            var keycloakUser = await _userRepository.GetUserAsync(tenant, user.Username, accessToken);
            await _userRepository.ResetPasswordAsync(tenant, keycloakUser.Data.Id!, user.Password, accessToken);
        }
    }
}
