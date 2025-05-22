using Feijuca.Auth.Models;

namespace Feijuca.Auth.Services;

public interface ITenantService
{
    IEnumerable<Tenant> Tenants { get; }

    Tenant Tenant { get; }

    User User { get; }

    string GetInfo(string infoName);

    /// <summary>
    /// Retrieves the list of tenants available for the current context.
    /// </summary>
    /// <param name="jobExecution">
    /// If set to <c>true</c>, indicates that the method is being executed in a background job or external process,
    /// where no HTTP context or authentication token is available. In this case, the method returns the tenant(s)
    /// previously defined via the <c>SetTenant</c> method.
    /// If set to <c>false</c>, the method extracts tenant information from the current HTTP context (e.g., from the authentication token).
    /// </param>
    /// <returns>
    /// A collection of <see cref="Tenant"/> objects representing the tenants associated with the current request or execution context.
    /// </returns>
    IEnumerable<Tenant> GetTenants(bool jobExecution = false);

    /// <summary>
    /// Retrieves a single tenant for the current context.
    /// </summary>
    /// <param name="jobExecution">
    /// If set to <c>true</c>, indicates that the method is being executed outside the HTTP pipeline,
    /// such as in a background job or scheduled task. In this case, the tenant previously defined using the
    /// <c>SetTenant</c> method will be returned.
    /// If set to <c>false</c>, the method attempts to extract the tenant from the current HTTP context (e.g., via authentication token).
    /// </param>
    /// <returns>
    /// A <see cref="Tenant"/> object representing the tenant associated with the current execution context.
    /// </returns>
    Tenant GetTenant(bool jobExecution = false);

    User GetUser();

    string GetToken();

    void SetTenants(IEnumerable<Tenant> tenants);

    void SetUser(User user);
}
