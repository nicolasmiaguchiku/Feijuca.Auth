using Feijuca.Auth.Common.Models;
using Feijuca.Auth.Infra.CrossCutting.Filters;
using Flurl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Feijuca.Auth.Infra.CrossCutting.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, KeycloakSettings? keycloakSettings)
        {
            var realmName = keycloakSettings?.Realms?.FirstOrDefault(x => x.DefaultSwaggerTokenGeneration)?.Name ?? "";

            if (keycloakSettings is not null && !string.IsNullOrEmpty(realmName))
            {
                var url = keycloakSettings.ServerSettings.Url
                    .AppendPathSegment("realms")
                    .AppendPathSegment(realmName)
                    .AppendPathSegment($"/protocol/openid-connect/token");

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Feijuca.Auth.Api", Version = "v1" });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "Inform a token JWT valid following format: Bearer {token}",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });

                    c.OperationFilter<AuthorizeCheckOperationFilter>();
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Feijuca.Auth.Api.xml"));
                });
            }

            services.AddSwaggerGen();
            return services;
        }
    }
}
