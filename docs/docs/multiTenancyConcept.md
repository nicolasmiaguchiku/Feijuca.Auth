### 🏢 Multi-Tenancy Concept

We've arrived at what is possibly the most powerful feature of **Feijuca.Auth**: the **Multi-Tenancy Concept**.

As you’ve seen earlier, when configuring your API, you can pass an array of realms. Each **realm** essentially represents a **tenant**.

For example:

- Tenant `10444` might represent **Company X**, with its own users, roles, and permission rules. Since it has a dedicated realm, it can configure it however it wants.
- Tenant `10445` may have completely different rules and requirements — and that’s perfectly fine. Its realm is isolated and independent.

This gives each tenant full control over its authentication and authorization configuration — a powerful and flexible approach!


### ⚙️ How to Configure Multi-Tenancy

Setting this up in **Feijuca.Auth** is very simple. Just add multiple realms to your `appsettings.json`. Here’s what it looks like:

```json
{
  "Settings": {
    "Realms": [
      {
        "Name": "10444",
        "Issuer": "https://keycloak.ul0sru.host/realms/10444"
      },
      {
        "Name": "10445",
        "Issuer": "https://keycloak.ul0sru.host/realms/10445"
      }
    ]
  }
}
```

With this configuration, your API will now accept tokens issued from **both** realms.




### 🚀 Propagating the Tenant

To make tenant information available throughout the API, you can use the built-in middleware:

```csharp
app.UseTenantMiddleware();
```

### 🧩 Tenant Propagation Inside Your API

When using this middleware, the tenant information is propagated along with the user related to the token.  
You can access this data via the `ITenantService` interface, which provides the following methods:

```csharp
string GetInfo(string infoName);
IEnumerable<Tenant> GetTenants();
Tenant GetTenant();
void SetTenant(Tenant tenant);
User GetUser();
string GetToken();
void SetTenants(IEnumerable<Tenant> tenants);
void SetUser(User user);
```

Each method serves a specific purpose and can be used according to your needs. The methods with Set prefix has the purpose to be used internally by the Middleware, you do not need use them. Enjoy the methods with Get prefix.

You also have access to the `Tenant` and `User` class models, allowing you to persist related data in your database and retrieve it when needed.

Since the tenant context is propagated on every request, you’re free to use it however best suits your application’s logic.

### ✅ Tenant and User Access in Endpoints

With the tenant middleware enabled and the `ITenantService` injected, you can now access both the tenant and user information inside your endpoints. This is particularly useful for logging, filtering data, or applying business rules per tenant or user context.

Here’s a practical example of how to retrieve tenant and user information from the current request:

```csharp
[ApiController]
[Route("[controller]")]
public class FeijucaExampleController(ITenantService tenantService) : ControllerBase
{
    [HttpGet("token-validation")]
    [Authorize]
    public ActionResult<string> SimpleAuthentication()
    {
        var tenant = tenantService.GetTenant();
        var user = tenantService.GetUser();

        return Ok($"Hello from Feijuca! 🎉 You are authenticated. Tenant (Realm): {tenant.Name}, Username: {user.Username}, UserID: {user.Id}");
    }

    [HttpGet("role-validation")]
    [Authorize]
    [RequiredRole("RoleName")]
    public ActionResult<string> RoleAuthentication()
    {
        var tenant = tenantService.GetTenant();
        var user = tenantService.GetUser();

        return Ok($"Hello from Feijuca! 🎉 You are authenticated with a valid role. Tenant (Realm): {tenant.Name} Username: {user.Username}, UserID: {user.Id}");
    }
}
```

### 📬 Want to know more?

Multi-tenancy can unlock powerful possibilities when building systems for different organizations or clients, but it can also introduce architectural complexities.

If you’re looking for advanced guidance, architectural recommendations, or real-world usage examples, feel free to reach out.

You can contact me directly through:

- [LinkedIn](https://www.linkedin.com/in/felipemattioli/)
- 📧: felipe-mattioli98@hotmail.com

I'd be happy to help or clarify any questions you might have.