using Feijuca.Auth.Models;
using Feijuca.Auth.Domain.Interfaces;
using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Providers;

namespace Feijuca.Auth.Application.Commands.GroupUser
{
    public class AddUserToGroupCommandHandler(IGroupUsersRepository userGroupRepository, ITenantProvider tenantProvider) : ICommandHandler<AddUserToGroupCommand, Result<bool>>
    {
        private readonly IGroupUsersRepository _userGroupRepository = userGroupRepository;

        public async Task<Result<bool>> HandleAsync(AddUserToGroupCommand request, CancellationToken cancellationToken)
        {
            var result = await _userGroupRepository.AddUserToGroupAsync(request.UserId, tenantProvider.Tenant.Name, request.GroupId, cancellationToken);

            if (result.IsSuccess)
            {
                return Result<bool>.Success(result.Data);
            }

            return Result<bool>.Failure(result.Error);
        }
    }
}
