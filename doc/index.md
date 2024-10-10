# WIP

## What is the purpose of Feijuca.Keycloak.AuthServices?

The name **Feijuca** comes from the famous Brazilian dish **Feijoada**. Since we’re Brazilian, we wanted to pick a name that’s fun and familiar, connecting with our culture.

Now, let’s talk about the project. **Feijuca** is designed to simplify working with Keycloak by offering two main modules: **Feijuca.Keycloak.TokenManager** and **Feijuca.Keycloak.AuthServices**. These modules streamline common Keycloak tasks, whether you’re managing users or handling multi-tenancy, making your integration with Keycloak smoother and less complex.

> **But don’t worry—if you only need one of the modules, feel free to use them separately! They are designed to work independently or together, depending on your needs.**

---

## Feijuca.Keycloak.TokenManager

**Feijuca.Keycloak.TokenManager** is an API that abstracts, simplifies, and centralizes the calls needed to interact with Keycloak. Instead of dealing with multiple endpoints to perform basic tasks, this module provides a set of predefined endpoints that cover a wide range of user-related actions in one place. Over time, the goal is to encapsulate even more of Keycloak's functionality, reducing the complexity of directly using its API.

### Features ⛲

- **All actions in one place:** Forget about calling multiple endpoints to manage users in Keycloak. With **Feijuca.Keycloak.TokenManager**, you can perform user-related actions (creation, deletion, email confirmation, password resets, etc.) through a single set of simplified endpoints.
  
- **Custom endpoints:** If there’s a feature you think would improve the project, feel free to open a PR and suggest additional custom endpoints to better suit your needs.

---

## Feijuca.Keycloak.AuthServices

The **Feijuca.Keycloak.AuthServices** module focuses on implementing **multi-tenancy** with Keycloak. It allows you to treat each Keycloak realm as a distinct tenant, making it easy to manage multiple tenants in your application, each with their own users, roles, and groups. This module simplifies the process by enabling you to configure multi-tenancy with just a few settings in your `appsettings` and through the use of a NuGet package.

### Features ⛲

- **Multi-tenancy through realms:** Manage multiple tenants within the same Keycloak server, where each realm acts as a unique tenant. This allows for different configurations and isolated spaces for each tenant.

- **Token management:** Retrieve information from tokens, such as claims, the tenant (realm) the token belongs to, and the associated user, all through easy-to-use endpoints.

---

These two modules provide everything you need to handle user management and multi-tenancy with Keycloak in a more streamlined and efficient way. Use them together for a full-featured experience, or separately depending on your specific needs. Plus, the project is open to contributions—if you think of a new feature that could enhance the experience, you’re welcome to submit a PR!

---

Feel free to explore the documentation to get the most out of **Feijuca**. If you have any questions or run into any issues, don’t hesitate to reach out to us at **ajuda@coderaw.io**—we’re here to help! Since this project belongs to you as well, it is open-sourced. 

This project is maintained by **Coderaw**, a company that specializes in building custom software solutions, offering consultancy services, and developing its own independent systems (SaaS). We're committed to making your experience with Keycloak simpler and more efficient.
