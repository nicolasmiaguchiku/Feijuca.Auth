using Feijuca.Auth.Application.Responses;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;

using MediatR;

namespace Feijuca.Auth.Application.Commands.Auth
{
    public class RefreshTokenCommandHandler(IUserRepository userRepository) : IRequestHandler<RefreshTokenCommand, Result<TokenDetailsResponse>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<TokenDetailsResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var tokeDetails = await _userRepository.RefreshTokenAsync(request.RefreshToken);
            

            return Result<TokenDetailsResponse>.Failure(tokeDetails.Error);
        }
    }
}
