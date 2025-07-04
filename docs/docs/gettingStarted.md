## 🔒 Feijuca.Auth — Auth & RBAC Made Simple for .NET

**Feijuca.Auth** is a robust and flexible solution tailored for applications running on supported .NET versions, providing seamless integration via a dedicated NuGet package. With developer-oriented features, it simplifies authentication and authorization by offering native capabilities to secure your APIs through both token validation and role-based access control (RBAC).

Once integrated, Feijuca.Auth enables you to **protect API endpoints effortlessly**, automatically handling:

- **401 Unauthorized** responses for requests missing a valid token.
- **403 Forbidden** responses when the token is present but lacks the required permissions.

In addition to standard secured routes, **custom public routes** can also be defined using the provided middleware — allowing developers to explicitly expose certain endpoints **without requiring authentication**, offering fine-grained control over access behavior.

---

### ⚙️ How It Works in .NET Projects

When you install the `Feijuca.Auth` NuGet package in a .NET project, it acts as the execution layer that connects your application to Keycloak’s authorization logic. This package enables:

- Middleware-based protection for controllers and endpoints.
- Token decoding and validation.
- Attribute-based role enforcement directly within your codebase.

> ✅ **Important**: To make this authorization system function correctly, you must also use the **Feijuca.Auth.Api** module.

This secondary module serves as a thin wrapper over Keycloak's REST API, allowing any system (not just .NET) to interact with Keycloak more cleanly. With it, you can:

- Create and manage user groups.
- Define roles and permissions.
- Assign roles to groups and users.
- And more.

This separation ensures a clean distinction between:

| Module              | Responsibility                                             |
|---------------------|-------------------------------------------------------------|
| `Feijuca.Auth`      | Enforces rules in your .NET application (middleware, tokens, attributes) |
| `Feijuca.Auth.Api`  | Manages rules, users, groups, roles and much more via HTTP endpoints  |

---

### 🧭 Summary

- ✅ **Using .NET?** Use both NuGet package for seamless route protection, token inspection, and custom middleware configurations and use REST API to handle actions on Keycloak.
- 🌐 **Using another language?** Interact with Feijuca.Auth through the REST API to handle actions on Keycloak.
- 🔐 Whether you need clearer definitions for Keycloak endpoints or a complete RBAC system, **Feijuca.Auth is designed to be your end-to-end solution** for authentication and authorization.

---

Need help? Check out the configuration docs or reach out directly.

---

### 🔧 Prerequisites

- An instance of a **Keycloak** server.
- An instance of a **MongoDB** server.  
  _(No worries — [MongoDB Atlas](https://www.mongodb.com/atlas/database) offers a free tier you can use to get started. Feel free to contribute by adding support for your favorite database!)_

---

### 🖥️ Let's Get Started!

- Click **[Here ➡️](/Feijuca.Auth/docs/keycloakMandatoryConfigs.html)** to continue and begin setting up your environment.



