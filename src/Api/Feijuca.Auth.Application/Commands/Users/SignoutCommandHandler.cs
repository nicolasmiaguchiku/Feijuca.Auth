using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Users
{
    public class SignoutCommandHandler(IUserRepository userRepository) : IRequestHandler<SignoutCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        public async Task<Result<bool>> Handle(SignoutCommand request, CancellationToken cancellationToken)
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
