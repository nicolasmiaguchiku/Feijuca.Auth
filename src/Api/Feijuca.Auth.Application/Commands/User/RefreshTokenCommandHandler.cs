using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Models;
using Feijuca.Auth.Domain.Interfaces;
using Feijuca.Auth.Http.Responses;
using LiteBus.Commands.Abstractions;

namespace Feijuca.Auth.Application.Commands.User
{
    public class RefreshTokenCommandHandler(IUserRepository userRepository) : ICommandHandler<RefreshTokenCommand, Result<TokenDetailsResponse>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<TokenDetailsResponse>> HandleAsync(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var tokenDetails = await _userRepository.RefreshTokenAsync(request.RefreshToken, cancellationToken);

            if (tokenDetails.IsSuccess)
            {
                return Result<TokenDetailsResponse>.Success(tokenDetails.Data.ToTokenDetailResponse());
            }

            return Result<TokenDetailsResponse>.Failure(tokenDetails.Error);
        }
    }
}
