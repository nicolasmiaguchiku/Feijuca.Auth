### 💡 Considerations

**Feijuca.Auth** is designed to work seamlessly with applications built on supported .NET versions, offering native integration and developer-friendly features through its NuGet package. This package provides out-of-the-box capabilities to protect your APIs and simplify authentication workflows within .NET environments.

However, the underlying **Feijuca.Auth.Api** module is built as a thin wrapper over Keycloak’s REST endpoints. This means that any application — regardless of programming language — can interact with Feijuca.Auth by making HTTP requests to manage users, groups, sessions, and tokens. You don’t need .NET in your stack to benefit from these capabilities.

## Summary

- ✅ If you're using **.NET**, you can leverage the **NuGet package** for deeper integration and streamlined security.
- 🌐 If you're using **another language**, you can still interact with the **Feijuca.Auth API** directly as a RESTful interface to Keycloak.

As you continue through the documentation, you’ll see how each usage scenario is supported and how to get the most out of the platform depending on your tech stack.

---

### 🔧 Prerequisites

- An instance of a **Keycloak** server.
- An instance of a **MongoDB** server.  
  _(No worries — [MongoDB Atlas](https://www.mongodb.com/atlas/database) offers a free tier you can use to get started. Feel free to contribute by adding support for your favorite database!)_

---

### 🖥️ Let's Get Started!

- Click **[Here ➡️](/Feijuca.Auth/docs/keycloakMandatoryConfigs.html)** to continue and begin setting up your environment.



