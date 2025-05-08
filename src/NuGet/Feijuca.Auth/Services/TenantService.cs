using Feijuca.Auth.Models;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Feijuca.Auth.Services;

public class TenantService(IHttpContextAccessor httpContextAccessor, JwtSecurityTokenHandler jwtSecurityTokenHandler) : ITenantService
{

    private IEnumerable<Tenant> _tenants = [];
    private User _userId = null!;

    public IEnumerable<Tenant> Tenants => _tenants;
    public User User => _userId;

    public IEnumerable<Tenant> GetTenantsFromToken()
    {
        string jwtToken = GetToken();
        if (!string.IsNullOrEmpty(jwtToken))
        {
            var tokenInfos = jwtSecurityTokenHandler.ReadJwtToken(jwtToken);
            var tenantClaim = tokenInfos.Claims.FirstOrDefault(c => c.Type == "tenant")?.Value!;

            var tenants = tenantClaim.Split(',').SelectMany(x =>
            {
                return new List<Tenant> { new(x) };
            });

            return tenants;


        }

        return [];
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

    public void SetTenants(IEnumerable<Tenant> tenants)
    {
        _tenants = tenants;
    }

    public void SetUser(User user)
    {
        _userId = user;
    }
}
