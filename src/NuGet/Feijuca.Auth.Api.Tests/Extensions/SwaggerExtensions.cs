using Feijuca.Auth.Api.Tests.Filters;
using Feijuca.Auth.Models;

using Flurl;

using Microsoft.OpenApi.Models;

namespace Feijuca.Auth.Api.Tests.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services, ServerSettings keyCloakSettings)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Receipts.CommandHandler.API", Version = "v1" });

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri(keyCloakSettings.Url.AppendPathSegment("/realms/10000/protocol/openid-connect/token")),
                        }
                    }
                });

                c.OperationFilter<AuthorizeCheckOperationFilter>();
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Receipts.CommandHandler.API.xml"));
            });
        }
    }
}
