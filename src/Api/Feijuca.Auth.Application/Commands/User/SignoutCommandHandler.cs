using Feijuca.Auth.Common.Errors;
using Mattioli.Configurations.Models;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.User
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
