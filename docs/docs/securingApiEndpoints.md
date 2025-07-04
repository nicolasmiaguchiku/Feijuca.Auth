
### Step 1: Basic Endpoint Protection

Protecting your endpoints is straightforward. **Feijuca** leverages the same logic as **Microsoft Identity** — you simply need to add the `[Authorize]` attribute to your controller actions.

If you don’t add this attribute, the endpoint will be publicly accessible and won’t require any token.

Here’s an example of a protected endpoint:

```csharp
[ApiController]
[Route("[controller]")]
public class FeijucaExampleController() : ControllerBase
{
    [HttpGet]
    [Authorize]
    public ActionResult<string> Get()
    {
        return Ok("Hello from Feijuca!!! Congratulations, you are authenticated!");
    }
}
```

### Step 2: How Feijuca Handles Token Validation

When a request is made to a protected endpoint (i.e., one with the `[Authorize]` attribute), Feijuca triggers an internal event that performs several validations on the token.

Here's what Feijuca checks:

1. **Token Presence**: It ensures that a token is included in the request.
2. **Token Expiration**: It checks if the token is still valid and not expired.
3. **Realm Matching**: It verifies whether the realm in the token matches one of the configured realms in your `appsettings.json`.
4. **Issuer Verification**: It confirms that the token's issuer matches the expected `Issuer` configured for that realm.

> ✅ If everything is correctly configured, you don’t need to worry — Feijuca will handle these checks automatically.

Tokens issued by **Keycloak** are signed and secure, providing strong protection for your API.

Once a valid token is passed, the request proceeds and the user can access the protected resource.



### Step 3: Securing Endpoints by Role

Feijuca allows you to go beyond basic authentication and enforce **role-based access control** using the `[RequiredRole]` attribute.

This attribute is specific to **Feijuca.Auth** and enables you to restrict access to users who have specific roles assigned in Keycloak.

### 🔐 Example: Role-Protected Endpoint

```csharp
[HttpGet]
[Authorize]
[RequiredRole("RoleName")]
public ActionResult<string> RoleAuthentication()
{
    return Ok("Hello from Feijuca! You have the required role.");
}
```

### 🛠 How to Set It Up

##### 1. Create the Role in Keycloak  
You can create roles directly in **Keycloak** or use the **Feijuca.Auth REST API** to manage them programmatically.

##### 2. Assign the Role  
Assign the role to a **user** or a **group** within Keycloak.

##### 3. Feijuca Will Do the Rest  
Feijuca will automatically check if the user has the required role when accessing endpoints decorated with:



### ✅ Step 4: Wrapping Up

By following the previous steps, you now have:

- ✔️ Authentication and authorization powered by Keycloak
- ✔️ Ability to protect endpoints using `[Authorize]`
- ✔️ Role-based access control via `[RequiredRole("...")]`
- ✔️ Optionally unprotected routes by choosing to use none of the attributes shown

Feijuca.Auth integrates seamlessly with the .NET authentication pipeline and follows conventions you're already familiar with from Microsoft Identity.

### 🔗 Full Example

You can find a complete example of how is the token validtion logic [here](https://github.com/fmattioli/Feijuca.Auth/blob/main/src/NuGet/Feijuca.Auth/Extensions/TenantAuthExtensions.cs).

---

Click **Next** to see how you can use the Feijuca.Auth on a multi-tenancy concept.

