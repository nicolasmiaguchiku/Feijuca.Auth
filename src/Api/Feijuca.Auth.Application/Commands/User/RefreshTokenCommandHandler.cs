using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Application.Responses;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.User
{
    public class RefreshTokenCommandHandler(IUserRepository userRepository) : IRequestHandler<RefreshTokenCommand, Result<TokenDetailsResponse>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<TokenDetailsResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var tokenDetails = await _userRepository.RefreshTokenAsync(request.RefreshToken, cancellationToken);

            if (tokenDetails.IsSuccess)
            {
                return Result<TokenDetailsResponse>.Success(tokenDetails.Response.ToTokenDetailResponse());
            }

            return Result<TokenDetailsResponse>.Failure(tokenDetails.Error);
        }
    }
}
