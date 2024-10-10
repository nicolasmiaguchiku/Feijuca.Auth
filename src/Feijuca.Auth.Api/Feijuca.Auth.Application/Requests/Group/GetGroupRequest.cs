using Microsoft.AspNetCore.Mvc;

namespace Application.Requests.Group
{
    public class GetGroupRequest
    {
        [FromQuery]
        public Guid GroupId { get; set; }
    }
}
