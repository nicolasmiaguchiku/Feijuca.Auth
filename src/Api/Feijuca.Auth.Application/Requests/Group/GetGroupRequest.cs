using Microsoft.AspNetCore.Mvc;

namespace Feijuca.Auth.Application.Requests.Group
{
    public class GetGroupRequest
    {
        [FromQuery]
        public Guid GroupId { get; set; }
    }
}
