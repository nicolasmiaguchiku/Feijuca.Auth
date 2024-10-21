using Feijuca.Auth.Models;
using Feijuca.Auth.Services;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Feijuca.Auth.Extensions
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddKeyCloakAuth(this IServiceCollection services, 
            Client client, 
            ServerSettings serverSettings, 
            IEnumerable<Realm> realms,
            IEnumerable<Policy>? policies = null) 
        {
            services
                .AddSingleton<JwtSecurityTokenHandler>()
                .AddScoped<IAuthService, AuthService>()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddKeycloakWebApi(
                    options =>
                    {
                        options.Resource = client.ClientId;
                        options.AuthServerUrl = serverSettings.Url;
                        options.VerifyTokenAudience = true;
                    },
                    options =>
                    {
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = OnMessageReceived(realms),
                            OnAuthenticationFailed = OnAuthenticationFailed,
                            OnChallenge = OnChallenge
                        };
                    });

            ConfigureAuthorization(services, client, policies);

            return services;
        }

        private static Func<MessageReceivedContext, Task> OnMessageReceived(IEnumerable<Realm> realms)
        {
            return async context =>
            {
                try
                {
                    var tokenJwt = context.Request.Headers.Authorization.FirstOrDefault();
                    if (!ValidateToken(context, tokenJwt))
                    {
                        return;
                    }

                    var token = tokenJwt!.Replace("Bearer ", "");
                    var tokenInfos = new JwtSecurityTokenHandler().ReadJwtToken(token);

                    if (!ValidateExpiration(context, tokenInfos) || !ValidateAudience(context, tokenInfos))
                    {
                        return;
                    }

                    var tenantNumber = tokenInfos.Claims.FirstOrDefault(c => c.Type == "tenant")?.Value;
                    var tenantRealm = realms.FirstOrDefault(realm => realm.Name == tenantNumber);
                    if (!ValidateRealm(context, tenantRealm))
                    {
                        return;
                    }

                    var tokenValidationParameters = await GetTokenValidationParameters(tenantRealm!);
                    var claims = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out var _);

                    context.Principal = claims;
                    context.Success();
                }
                catch (Exception e)
                {
                    context.Response.StatusCode = 500;
                    context.HttpContext.Items["AuthError"] = $"Authentication error: {e.Message}";
                    await context.Response.WriteAsJsonAsync(new { error = e.Message });
                    context.Fail(e.Message);
                }
            };
        }

        private static Task OnAuthenticationFailed(AuthenticationFailedContext context)
        {
            var errorMessage = context.HttpContext.Items["AuthError"] as string ?? "Authentication failed!";
            var statusCode = context.HttpContext.Items["AuthStatusCode"] as int? ?? 401;
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsJsonAsync(new { error = errorMessage });
        }

        private static async Task OnChallenge(JwtBearerChallengeContext context)
        {
            if (!context.Response.HasStarted)
            {
                var errorMessage = context.HttpContext.Items["AuthError"] as string ?? "Authentication failed!";
                var statusCode = context.HttpContext.Items["AuthStatusCode"] as int? ?? 401;
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { Message = errorMessage });
            }

            context.HandleResponse();
        }

        private static bool ValidateToken(MessageReceivedContext context, string? tokenJwt)
        {
            if (string.IsNullOrEmpty(tokenJwt))
            {
                context.HttpContext.Items["AuthError"] = "Invalid JWT token!";
                context.HttpContext.Items["AuthStatusCode"] = 401;
                return false;
            }

            return true;
        }

        private static bool ValidateExpiration(MessageReceivedContext context, JwtSecurityToken tokenInfos)
        {
            var expirationClaim = tokenInfos.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;
            if (expirationClaim != null && long.TryParse(expirationClaim, out var expirationUnix))
            {
                var expirationDate = DateTimeOffset.FromUnixTimeSeconds(expirationUnix).UtcDateTime;
                if (DateTime.UtcNow >= expirationDate)
                {
                    context.Response.StatusCode = 401;
                    context.HttpContext.Items["AuthError"] = "Token has expired.";
                    context.Fail("Token expired");
                    return false;
                }
            }

            return true;
        }

        private static bool ValidateRealm(MessageReceivedContext context, Realm? tenantRealm)
        {
            if (tenantRealm == null)
            {
                context.HttpContext.Items["AuthError"] = "Invalid tenant!";
                context.HttpContext.Items["AuthStatusCode"] = 401;
                return false;
            }
            return true;
        }

        private static bool ValidateAudience(MessageReceivedContext context, JwtSecurityToken tokenInfos)
        {
            var audience = tokenInfos.Claims.FirstOrDefault(c => c.Type == "aud")?.Value;
            if (string.IsNullOrEmpty(audience))
            {
                context.HttpContext.Items["AuthError"] = "Invalid audience!";
                context.HttpContext.Items["AuthStatusCode"] = 403;
                return false;
            }
            return true;
        }

        private static async Task<TokenValidationParameters> GetTokenValidationParameters(Realm tenantRealm)
        {
            using var httpClient = new HttpClient();
            var jwksUrl = $"{tenantRealm.Issuer}/protocol/openid-connect/certs";
            var jwks = await httpClient.GetStringAsync(jwksUrl);
            var jsonWebKeySet = new JsonWebKeySet(jwks);

            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = tenantRealm.Issuer,
                ValidateAudience = true,
                ValidAudience = tenantRealm.Audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKeys = jsonWebKeySet.Keys
            };
        }

        private static void ConfigureAuthorization(IServiceCollection services, Client client, IEnumerable<Policy>? policySettings)
        {
            services
               .AddAuthorization()
               .AddKeycloakAuthorization();

            foreach(var policy in policySettings ?? [])
            {
                if (!string.IsNullOrEmpty(policy.Name))
                {
                    services
                        .AddAuthorizationBuilder()
                        .AddPolicy(policy.Name, p =>
                        {
                            p.RequireResourceRolesForClient(
                                client.ClientId,
                                policy.Roles!.ToArray());
                        });
                }
            }
        }
    }
}
