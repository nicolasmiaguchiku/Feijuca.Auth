using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Application.Responses;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Auth
{
    public class LoginCommandHandler(ILoginService loginService) : IRequestHandler<LoginCommand, Result<TokenDetailsResponse>>
    {
        private readonly ILoginService _loginService = loginService;

        public async Task<Result<TokenDetailsResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _loginService.LoginAsync(request.LoginUser.RevokeActiveSessions, request.LoginUser.Username, request.LoginUser.Password, cancellationToken);
            if (result.IsSuccess)
            {
                return Result<TokenDetailsResponse>.Success(result.Response.ToTokenDetailResponse());
            }

            return Result<TokenDetailsResponse>.Failure(result.Error);
        }
    }
}
