using Application.Mappers;
using Application.Responses;
using Common.Models;
using Domain.Interfaces;
using MediatR;

namespace Application.Commands.Auth
{
    public class LoginCommandHandler(ILoginService loginService) : IRequestHandler<LoginCommand, Result<TokenDetailsResponse>>
    {
        private readonly ILoginService _loginService = loginService;

        public async Task<Result<TokenDetailsResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _loginService.LoginAsync(request.LoginUser.RevokeActiveSessions, request.LoginUser.Username, request.LoginUser.Password);
            if (result.IsSuccess)
            {
                return Result<TokenDetailsResponse>.Success(result.Response.ToTokenDetailResponse());
            }

            return Result<TokenDetailsResponse>.Failure(result.Error);
        }
    }
}
