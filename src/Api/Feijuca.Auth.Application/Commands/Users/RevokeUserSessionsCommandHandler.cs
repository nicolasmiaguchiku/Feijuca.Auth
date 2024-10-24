using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Users
{
    public class RevokeUserSessionsCommandHandler(IUserRepository userRepository) : IRequestHandler<RevokeUserSessionsCommand, Result>
    {
        private readonly IUserRepository _userRepository = userRepository;
        public async Task<Result> Handle(RevokeUserSessionsCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.RevokeSessionsAsync(request.UserId, cancellationToken);

            if (result.IsSuccess)
            {
                return result;
            }

            return Result.Failure(result.Error);
        }
    }
}
