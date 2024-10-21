using Feijuca.Auth.Models;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Feijuca.Auth.Services
{
    public class AuthService(IHttpContextAccessor httpContextAccessor, JwtSecurityTokenHandler jwtSecurityTokenHandler) : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly JwtSecurityTokenHandler _tokenHandler = jwtSecurityTokenHandler;

        public string GetTenantFromToken()
        {
            string jwtToken = GetToken();
            var tokenInfos = _tokenHandler.ReadJwtToken(jwtToken);
            var tenantClaim = tokenInfos.Claims.FirstOrDefault(c => c.Type == "tenant")?.Value!;
            return tenantClaim;
        }

        public string GetInfoFromToken(string infoName)
        {
            string jwtToken = GetToken();
            var tokenInfos = _tokenHandler.ReadJwtToken(jwtToken);
            var userClaim = tokenInfos.Claims.FirstOrDefault(c => c.Type == infoName)?.Value!;
            return userClaim;
        }

        public Guid GetUserIdFromToken()
        {
            string jwtToken = GetToken();
            var tokenInfos = _tokenHandler.ReadJwtToken(jwtToken);
            var userClaim = tokenInfos.Claims.FirstOrDefault(c => c.Type == "sub")?.Value!;
            return Guid.Parse(userClaim);
        }
        
        private string GetToken()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.FirstOrDefault();
            return authorizationHeader!.Replace("Bearer ", string.Empty);
        }
    }
}
