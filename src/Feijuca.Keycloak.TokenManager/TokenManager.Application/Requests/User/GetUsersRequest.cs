using Application.Requests.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.User
{
    public class GetUsersRequest
    {
        public GetUsersRequest()
        {
            PageFilter = new PageFilterRequest { Page = 1, PageSize = 60, };
        }

        [FromQuery]
        public PageFilterRequest PageFilter { get; set; }

        [FromQuery]
        public IEnumerable<Guid>? Ids { get; set; }

        [FromQuery]
        public IEnumerable<string>? Emails { get; set; }
    }
}
