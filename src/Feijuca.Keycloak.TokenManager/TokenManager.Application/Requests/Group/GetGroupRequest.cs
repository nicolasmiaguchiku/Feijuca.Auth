using Microsoft.AspNetCore.Mvc;

namespace TokenManager.Application.Requests.Group
{
    public class GetGroupRequest
    {
        [FromQuery]
        public Guid GroupId { get; set; }
    }
}
