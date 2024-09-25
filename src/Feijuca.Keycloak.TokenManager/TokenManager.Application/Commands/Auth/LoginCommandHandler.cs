using MediatR;
using TokenManager.Application.Mappers;
using TokenManager.Application.Responses;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Commands.Auth
{
    public class LoginCommandHandler(IAuthRepository authRepository) : IRequestHandler<LoginCommand, Result<TokenDetailsResponse>>
    {
        private readonly IAuthRepository _authRepository = authRepository;

        public async Task<Result<TokenDetailsResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = request.LoginUser.ToLoginUserDomain();
            var tokenDetailsResult = await _authRepository.LoginAsync(request.Tenant, user);
            if (tokenDetailsResult.IsSuccess)
            {
                return tokenDetailsResult.ToTokenResponse();
            }

            return Result<TokenDetailsResponse>.Failure(tokenDetailsResult.Error);
        }
    }
}
