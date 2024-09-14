using MediatR;
using TokenManager.Application.Responses;
using TokenManager.Common.Models;
using TokenManager.Domain.Interfaces;

namespace TokenManager.Application.Queries
{
    public class GetAllGroupsQueryHandler(IGroupRepository groupRepository) : IRequestHandler<GetAllGroupsQuery, Result<IEnumerable<GroupResponse>>>
    {
        private IGroupRepository _groupRepository = groupRepository;

        public async Task<Result<IEnumerable<GroupResponse>>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups = await _groupRepository.GetAllGroups(request.Tenant);
            throw new Exception();
        }
    }
}
