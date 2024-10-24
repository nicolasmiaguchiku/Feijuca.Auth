using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Feijuca.Auth.Infra.CrossCutting.Filters;

public class TenantHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= (List<OpenApiParameter>) [];

        if (!operation.Parameters.Any(p => p.Name == "Tenant" && p.In == ParameterLocation.Header))
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Tenant",
                In = ParameterLocation.Header,
                Description = "The tenant identifier used to filter the clients within a specific Keycloak realm.",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
        }
    }
}