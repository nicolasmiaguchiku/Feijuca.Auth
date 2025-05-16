using Feijuca.Auth.Application.Mappers;
using Feijuca.Auth.Application.Responses;
using Feijuca.Auth.Common.Errors;
using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Domain.Interfaces;

using MediatR;

namespace Feijuca.Auth.Application.Queries.Groups
{
    public class GetAllGroupsQueryHandler(IGroupRepository groupRepository) : IRequestHandler<GetAllGroupsQuery, Result<IEnumerable<GroupResponse>>>
    {
        private readonly IGroupRepository _groupRepository = groupRepository;

        public async Task<Result<IEnumerable<GroupResponse>>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
        {
            var result = await _groupRepository.GetAllAsync(cancellationToken);

            if (result.IsSuccess)
            {
                var results = result.Response.Where(x => x.Name != "feijuca-auth-api");
                return Result<IEnumerable<GroupResponse>>.Success(results.ToResponse());
            }

            return Result<IEnumerable<GroupResponse>>.Failure(GroupErrors.CreationGroupError);
        }
    }
}
