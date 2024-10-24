using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Users
{
    public class CreateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.AddUserRequest.ToDomain();
            var result = await _userRepository.CreateAsync(user, cancellationToken);

            if (result.IsSuccess)
            {
                var keycloakUser = await _userRepository.GetAsync(user.Username, cancellationToken);
                result = await _userRepository.ResetPasswordAsync(keycloakUser.Response.Id, user.Password, cancellationToken);

                if (result.IsSuccess)
                {
                    return result;
                }
            }

            return Result.Failure(result.Error);
        }
    }
}
