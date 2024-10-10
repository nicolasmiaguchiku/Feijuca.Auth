using Application.Mappers;
using Application.Responses;
using Common.Models;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Auth
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
