using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.UserAttributes
{
    public class UpdateUserAttributesCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserAttributesCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpdateUserAttributesCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetAsync(request.Username, cancellationToken);

            var oldAttributesUpdated = user.Response.Attributes;

            foreach (var newAttribute in request.UpdateUserAttributeRequest.Attributes)
            {
                if (oldAttributesUpdated.TryGetValue(newAttribute.Key, out string[]? _))
                {
                    oldAttributesUpdated[newAttribute.Key] = newAttribute.Value;
                }
            }

            user.Response.Attributes = oldAttributesUpdated;

            var result = await userRepository.UpdateUserAsync(user.Response.Id, user.Response, cancellationToken);

            if (result.IsSuccess)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(UserErrors.ErrorWhileAddedUserAttribute);
        }
    }
}
