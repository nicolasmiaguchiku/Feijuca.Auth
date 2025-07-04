### 📦 Step 1: Install the NuGet Package

Start by installing the **Feijuca.Auth** package from NuGet:

```bash
dotnet add package Feijuca.Auth
```

You can also find it [on NuGet.org](https://www.nuget.org/packages/Feijuca.Auth).

This package contains all the necessary components to enable Keycloak-based authentication and authorization in your API.

---

### ⚙️ Step 2: Configure the `appsettings.json`

After installing the package, it's time to configure your `appsettings.json` file.  
You need to define an array of realms that your application will support.

> 💡 This allows **Feijuca.Auth** to support multi-tenant scenarios.  
> For now, we’ll configure a **single-tenant** example.

Here’s a minimal configuration example:

```json
"Realms": [
  {
    "Name": "smartconsig",
    "Issuer": "https://keycloak-instance-url/realms/your-created-realm"
  }
]
```

- **Name**: An internal identifier for your realm.
- **Issuer**: The full URL to your Keycloak realm’s issuer, typically `https://<host>/realms/<realm-name>`.

This section is required and will be passed to **Feijuca** when initializing the authentication layer.

---

### 🧩 Step 3: Register Feijuca in `Program.cs`

Now that your `appsettings.json` is configured, it's time to enable Feijuca in your application.

Inside your `Program.cs`, register the authentication services by calling the following extension method:

```csharp
builder.Services.AddApiAuthentication(applicationSettings.Realms);
```

This method will:

- Automatically register all necessary **JWT authentication** and **authorization** services.
- Load your configured realms and validate tokens issued by them.

> 💡 You should ensure that `applicationSettings.Realms` contains the same configuration from your `appsettings.json`.


---

### 🛡️ Step 4: Configure Public (Unprotected) Routes

By default, **Feijuca.Auth** will protect all your API routes with authentication and authorization.

However, in some cases — like webhooks or public endpoints — you might want certain routes to be accessible **without requiring a token**.  
To support this, you can use the provided middleware.

Add the following to your `Program.cs` file:

```csharp
app.UseTenantMiddleware(options =>
{
    options.AvailableUrls = ["webhook"];
});
```

This tells **Feijuca** to **ignore authentication and authorization** for any route that contains `"webhook"` in its path.

✅ Use this when you need external systems to send requests without authentication.

---

Alternatively, you can also use the `[AllowAnonymous]` or `[Authorize]` attributes in your controllers to control access per endpoint, as you're used to in .NET.

---

Once this is done, your setup is complete! 🎉

Click **Next** to learn how to protect your endpoints — the process is very familiar if you’ve worked with `.NET` before.
