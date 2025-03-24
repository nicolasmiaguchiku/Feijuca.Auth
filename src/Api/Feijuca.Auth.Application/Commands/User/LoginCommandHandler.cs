using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Application.Responses;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.User
{
    public class LoginCommandHandler(IUserRepository userRepository) : IRequestHandler<LoginCommand, Result<TokenDetailsResponse>>
    {
        public async Task<Result<TokenDetailsResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await userRepository.LoginAsync(request.LoginUser.Username, request.LoginUser.Password, cancellationToken);

            return result.IsSuccess ? Result<TokenDetailsResponse>.Success(result.Response.ToTokenDetailResponse()) : Result<TokenDetailsResponse>.Failure(result.Error);
        }
    }
}
