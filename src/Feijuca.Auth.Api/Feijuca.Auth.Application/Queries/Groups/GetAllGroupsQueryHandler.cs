using Application.Mappers;
using Application.Responses;
using Common.Errors;
using Common.Models;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries.Groups
{
    public class GetAllGroupsQueryHandler(IGroupRepository groupRepository) : IRequestHandler<GetAllGroupsQuery, Result<IEnumerable<GroupResponse>>>
    {
        private readonly IGroupRepository _groupRepository = groupRepository;

        public async Task<Result<IEnumerable<GroupResponse>>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
        {
            var result = await _groupRepository.GetAllAsync(request.Tenant);

            if (result.IsSuccess)
            {
                return Result<IEnumerable<GroupResponse>>.Success(result.Response.ToResponse());
            }

            return Result<IEnumerable<GroupResponse>>.Failure(GroupErrors.CreationGroupError);
        }
    }
}
