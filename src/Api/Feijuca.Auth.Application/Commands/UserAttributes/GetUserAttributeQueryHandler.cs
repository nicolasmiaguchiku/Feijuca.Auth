using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;
using MediatR;

namespace Feijuca.Auth.Application.Commands.UserAttributes
{
    public class GetUserAttributeQueryHandler(IUserRepository userRepository, ITenantService tenantService) : IRequestHandler<GetUserAttributeQuery, Result<Dictionary<string, string[]>>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Result<Dictionary<string, string[]>>> Handle(GetUserAttributeQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetAsync(request.Username, cancellationToken);

            if (!result.IsSuccess)
            {
                return Result<Dictionary<string, string[]>>.Failure(UserErrors.GetAllUsersError);
            }

            var attributes = result.Response.Attributes;
            return Result<Dictionary<string, string[]>>.Success(attributes);
        }


    }
}
