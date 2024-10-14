## What is **Feijuca.Auth**?

Inspired by the iconic Brazilian dish [Feijoada](https://theculturetrip.com/south-america/brazil/articles/a-brief-introduction-to-feijoada-brazils-national-dish), the name **Feijuca** reflects our Brazilian roots and cultural heritage. **Feijuca** is a playful nickname for **Feijoada**, embodying the spirit of our project: a fusion of functionality and culture.

**Feijuca.Auth** is a comprehensive solution designed to simplify integration with Keycloak, providing both a NuGet package and an API to streamline authentication, authorization, and access control management.

### Key Features:

- **NuGet Package**: 
  - Contains essential classes for configuring and managing the Feijuca.Auth project.
  - Allows you to define user permissions and validate JWT tokens, ensuring they are legitimate and issued by a trusted authority.
  - Supports integration with Keycloak in a multi-tenant model, where each realm in Keycloak serves as a tenant within your application, offering a scalable and secure solution for managing authentication and authorization across multiple tenants.

- **API**:
  - Offers a streamlined interface for managing interactions with Keycloak, focusing on:
    - **User Authentication**: Securely verify user identities.
    - **Authorization**: Manage user permissions and roles efficiently.
    - **Role-Based Access Control (RBAC)**: Implement and enforce access policies based on user roles.
    - **User Management**:
      - **Create and Remove Users**: Easily add or delete user accounts as needed.
      - **Manage Groups**: Create and remove user groups to organize access.
    - **User Operations**:
      - **Email Confirmation**: Ensure users verify their email addresses for account security.
      - **Password Reset**: Allow users to reset their passwords securely.
      - **Session Revocation**: Revoke user sessions to protect sensitive information and enhance security.

Together, these components of **Feijuca.Auth** aim to reduce the complexity of working with Keycloak, making user management and multi-tenancy more seamless and developer-friendly.

---

### Curious to know how this works? Check out the documentation!
[Documentation Link](https://example.com)

## Contributors

Meet the amazing people who helped build **Feijuca.Auth**! Connect with them on LinkedIn:

| **Name**                 | **LinkedIn**                                                              |
|--------------------------|---------------------------------------------------------------------------|
| Felipe Mattioli           | <a href="https://www.linkedin.com/in/felipemattioli/" target="_blank"><img src="https://cdn-icons-png.flaticon.com/512/174/174857.png" width="20"/> </a> |
| Wesley Souza              | <a href="https://www.linkedin.com/in/weslleyms/" target="_blank"><img src="https://cdn-icons-png.flaticon.com/512/174/174857.png" width="20"/> </a>  |
| Matheus Galvão            | <a href="https://www.linkedin.com/in/matheu-sandregalvaodasilva/" target="_blank"><img src="https://cdn-icons-png.flaticon.com/512/174/174857.png" width="20"/> </a> |

We’re grateful for everyone’s hard work and contributions!

Have a suggestion to improve the tool? Contribute to the project by opening a [PR](https://github.com/coderaw-io/Feijuca.Auth/pulls).
