using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Models;
using Feijuca.Auth.Domain.Interfaces;
using LiteBus.Commands.Abstractions;

namespace Feijuca.Auth.Application.Commands.User
{
    public class SignoutCommandHandler(IUserRepository userRepository) : ICommandHandler<SignoutCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        public async Task<Result<bool>> HandleAsync(SignoutCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.SignoutAsync(request.RefreshToken, cancellationToken);
            if (result.IsSuccess)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(UserErrors.InvalidRefreshToken);
        }
    }
}
