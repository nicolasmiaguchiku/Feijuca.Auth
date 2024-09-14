using MediatR;

using TokenManager.Application.Mappers;
using TokenManager.Application.Services.Responses;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Commands.Users
{
    public class LoginUserCommandHandler(IUserRepository userRepository) : IRequestHandler<LoginUserCommand, Result<TokenDetailsResponse>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<TokenDetailsResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.LoginUser.ToDomain();
            var tokenDetailsResult = await _userRepository.LoginAsync(request.Tenant, user);
            if (tokenDetailsResult.IsSuccess)
            {
                return tokenDetailsResult.ToTokenResponse();
            }

            return Result<TokenDetailsResponse>.Failure(tokenDetailsResult.Error);
        }
    }
}
