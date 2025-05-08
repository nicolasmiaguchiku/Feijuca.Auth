using Feijuca.Auth.Models;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Feijuca.Auth.Services;

public class TenantService(IHttpContextAccessor httpContextAccessor, JwtSecurityTokenHandler jwtSecurityTokenHandler) : ITenantService
{

    private Tenant _tenant = null!;
    private User _userId = null!;

    public Tenant Tenant => _tenant;
    public User User => _userId;

    public Tenant GetTenantFromToken()
    {
        string jwtToken = GetToken();
        if (!string.IsNullOrEmpty(jwtToken))
        {
            var tokenInfos = jwtSecurityTokenHandler.ReadJwtToken(jwtToken);
            var tenantClaim = tokenInfos.Claims.FirstOrDefault(c => c.Type == "tenant")?.Value!;
            return new Tenant(tenantClaim.Split(','));
        }

        return new Tenant(["Invalid tenant"]);
    }

    public string GetInfoFromToken(string infoName)
    {
        string jwtToken = GetToken();
        if (!string.IsNullOrEmpty(jwtToken))
        {
            var tokenInfos = jwtSecurityTokenHandler.ReadJwtToken(jwtToken);
            var userClaim = tokenInfos.Claims.FirstOrDefault(c => c.Type == infoName)?.Value!;
            return userClaim;
        }

        return string.Empty;
    }

    public User GetUserFromToken()
    {
        string jwtToken = GetToken();
        if (!string.IsNullOrEmpty(jwtToken))
        {
            var tokenInfos = jwtSecurityTokenHandler.ReadJwtToken(jwtToken);
            var userId = tokenInfos.Claims.FirstOrDefault(c => c.Type == "sub")?.Value!;
            var userName = tokenInfos.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value!;
            return new User(Guid.Parse(userId), userName);
        }

        return new User(Guid.Empty, string.Empty);
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

    public void SetTenant(Tenant tenant)
    {
        _tenant = tenant;
    }

    public void SetUser(User user)
    {
        _userId = user;
    }
}
