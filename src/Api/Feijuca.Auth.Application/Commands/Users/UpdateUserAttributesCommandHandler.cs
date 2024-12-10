using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.Users
{
    public class UpdateUserAttributesCommandHandler(IUserRepository _userRepository) : IRequestHandler<UpdateUserAttributesCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpdateUserAttributesCommand request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.AddUserAttributesAsync(request.Id, request.Attributes, cancellationToken);
            return result;
        }
    }
}
