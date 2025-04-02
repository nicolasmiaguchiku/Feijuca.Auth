using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Feijuca.Auth.Services
{
    public class TenantService(IHttpContextAccessor httpContextAccessor, JwtSecurityTokenHandler jwtSecurityTokenHandler) : ITenantService
    {

        private string _tenant = string.Empty;
        private Guid _userId = Guid.Empty;

        public string Tenant => _tenant;
        public Guid UserId => _userId;

        public string GetTenantFromToken()
        {
            string jwtToken = GetToken();
            if (string.IsNullOrEmpty(jwtToken))
            {
                var tokenInfos = jwtSecurityTokenHandler.ReadJwtToken(jwtToken);
                var tenantClaim = tokenInfos.Claims.FirstOrDefault(c => c.Type == "tenant")?.Value!;
                return tenantClaim;
            }

            return string.Empty;
        }

        public string GetInfoFromToken(string infoName)
        {
            string jwtToken = GetToken();
            if (string.IsNullOrEmpty(jwtToken))
            {
                var tokenInfos = jwtSecurityTokenHandler.ReadJwtToken(jwtToken);
                var userClaim = tokenInfos.Claims.FirstOrDefault(c => c.Type == infoName)?.Value!;
                return userClaim;
            }

            return string.Empty;
        }

        public Guid GetUserIdFromToken()
        {
            string jwtToken = GetToken();
            if (string.IsNullOrEmpty(jwtToken))
            {
                var tokenInfos = jwtSecurityTokenHandler.ReadJwtToken(jwtToken);
                var userClaim = tokenInfos.Claims.FirstOrDefault(c => c.Type == "sub")?.Value!;
                return Guid.Parse(userClaim);
            }

            return Guid.Empty;
        }

        private string GetToken()
        {
            var authorizationHeader = httpContextAccessor.HttpContext?.Request.Headers.Authorization.FirstOrDefault();

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return string.Empty;
            }

            return authorizationHeader["Bearer ".Length..];
        }


        public void SetTenant(string tenantId)
        {
            _tenant = tenantId;
        }

        public void SetUser(Guid userId)
        {
            _userId = userId;
        }
    }
}
