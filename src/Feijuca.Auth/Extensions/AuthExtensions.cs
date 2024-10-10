using Feijuca.MultiTenancy.Services;
using Feijuca.MultiTenancy.Services.Models;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Feijuca.MultiTenancy.Extensions
{
    public static class AuthExtensions
    {
        private static readonly HttpClient _httpClient = new();
        private static readonly JwtSecurityTokenHandler _tokenHandler = new();

        public static IServiceCollection AddKeyCloakAuth(this IServiceCollection services, AuthSettings authSettings)
        {
            services
                .AddSingleton(_tokenHandler)
                .AddSingleton(authSettings)
                .AddScoped<IAuthService, AuthService>()
                .AddSingleton(authSettings)
                .AddScoped<IAuthService, AuthService>()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddKeycloakWebApi(
                    options =>
                    {
                        options.Resource = authSettings.ClientId;
                        options.AuthServerUrl = authSettings.AuthServerUrl;
                        options.VerifyTokenAudience = true;
                    },
                    options =>
                    {
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = OnMessageReceived(authSettings),
                            OnAuthenticationFailed = OnAuthenticationFailed,
                            OnChallenge = OnChallenge
                        };
                    });


            ConfigureAuthorization(services, authSettings);

            return services;
        }

        private static Func<MessageReceivedContext, Task> OnMessageReceived(AuthSettings authSettings)
        {
            return async context =>
            {
                try
                {
                    var tokenJwt = context.Request.Headers.Authorization.FirstOrDefault()!;
                    if (!ValidateToken(context, tokenJwt))
                    {
                        return;
                    }

                    var token = tokenJwt.Replace("Bearer ", "");
                    var tokenInfos = _tokenHandler.ReadJwtToken(token);

                    if (!ValidateAudience(context, tokenInfos))
                    {
                        return;
                    }

                    var tenantNumber = tokenInfos.Claims.FirstOrDefault(c => c.Type == "tenant")?.Value;
                    var tenantRealm = authSettings.Realms.FirstOrDefault(realm => realm.Name == tenantNumber)!;
                    if (!ValidateRealm(context, tenantRealm))
                    {
                        return;
                    }

                    var tokenValidationParameters = await GetTokenValidationParameters(tenantRealm);
                    var claims = _tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                    context.Principal = claims;
                    context.Success();
                }
                catch (Exception e)
                {
                    context.Response.StatusCode = 500;
                    context.HttpContext.Items["AuthError"] = $"Authentication error: {e.Message}";
                    context.Fail("");
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

        private static bool ValidateToken(MessageReceivedContext context, string tokenJwt)
        {
            if (string.IsNullOrEmpty(tokenJwt))
            {
                context.HttpContext.Items["AuthError"] = "Invalid JWT token!";
                context.HttpContext.Items["AuthStatusCode"] = 401;
                return false;
            }
            return true;
        }

        private static bool ValidateRealm(MessageReceivedContext context, Realm tenantRealm)
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
            var jwksUrl = $"{tenantRealm.Issuer}/protocol/openid-connect/certs";
            var jwks = await _httpClient.GetStringAsync(jwksUrl);
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

        private static void ConfigureAuthorization(IServiceCollection services, AuthSettings authSettings)
        {
            services
               .AddAuthorization()
               .AddKeycloakAuthorization();

            if (!string.IsNullOrEmpty(authSettings?.PolicyName))
            {
                services
                    .AddAuthorizationBuilder()
                    .AddPolicy(authSettings.PolicyName, policy =>
                    {
                        policy.RequireResourceRolesForClient(
                            authSettings.ClientId,
                            authSettings.Roles!.ToArray());
                    });
            }
        }
    }
}
