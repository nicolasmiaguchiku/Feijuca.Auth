using Microsoft.AspNetCore.Mvc;
using TokenManager.Application.Requests.Pagination;

namespace TokenManager.Application.Requests.GroupUsers
{
    public class GetUsersGroupRequest
    {
        public GetUsersGroupRequest()
        {
            PageFilter = new PageFilterRequest { Page = 1, PageSize = 60, };
        }

        [FromQuery]
        public PageFilterRequest PageFilter { get; set; }

        [FromQuery]
        public Guid GroupId { get; set; }

        [FromQuery]
        public IEnumerable<string>? Emails { get; set; }
    }
}
