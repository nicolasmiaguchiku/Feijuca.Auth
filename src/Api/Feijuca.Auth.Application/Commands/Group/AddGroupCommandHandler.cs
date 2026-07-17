using Feijuca.Auth.Models;
using Feijuca.Auth.Domain.Interfaces;
using LiteBus.Commands.Abstractions;
using Feijuca.Auth.Providers;

namespace Feijuca.Auth.Application.Commands.Group
{
    public class AddGroupCommandHandler(IGroupRepository groupRepository, ITenantProvider tenantProvider) : ICommandHandler<AddGroupCommand, Result<string>>
    {
        private readonly IGroupRepository _groupRepository = groupRepository;

        public async Task<Result<string>> HandleAsync(AddGroupCommand request, CancellationToken cancellationToken)
        {
            var result = await _groupRepository.CreateAsync(request.AddGroupRequest.Name,
                tenantProvider.Tenant.Name,
                request.AddGroupRequest.Attributes, 
                cancellationToken);

            return result;
        }
    }
}
