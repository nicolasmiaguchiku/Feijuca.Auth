using Feijuca.Auth.Attributes;
using Feijuca.Auth.Models;
using Feijuca.Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Feijuca.Auth.Api.Tests.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeijucaExampleController(ITenantService tenantService) : ControllerBase
    {
        [HttpGet("token-validation")]
        [Authorize]
        public ActionResult<string> SimpleAuthentication()
        {
            var tenant = tenantService.GetTenant();
            var user = tenantService.GetUser();

            return Ok($"Hello from Feijuca! 🎉 You are authenticated. Tenant (Realm): {tenant.Name}, Username: {user.Username}, UserID: {user.Id}");
        }

        [HttpGet("role-validation")]
        [Authorize]
        [RequiredRole("RoleName")]
        public ActionResult<string> RoleAuthentication()
        {
            var tenant = tenantService.GetTenant();
            var user = tenantService.GetUser();

            return Ok($"Hello from Feijuca! 🎉 You are authenticated with a valid role. Tenant (Realm): {tenant.Name} Username: {user.Username}, UserID: {user.Id}");
        }
    }
}
