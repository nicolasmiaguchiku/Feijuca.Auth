# 🔑 Configuring Permissions in Keycloak for API Integration

In order for your API to perform operations such as creating users, groups, realms, clients, roles, group roles, and more in Keycloak, you need to configure the appropriate permissions in the realm. This configuration is done by granting specific permissions to the **Service Account** associated with the client your API uses. Follow the steps below:

## 1. 🖥️ Access the Keycloak Admin Console
- Log in to the Keycloak Admin Console and select the realm where you want to configure permissions.

## 2. 📄 Define or Select the Client that will Represent Your API
- We recommend you create a new client dedicated to handling the operations.

## 3. 🔧 Access the **Service Account Roles** Tab
- On the client page, click on the **Service Account Roles** tab to manage the permissions for the service account.

## 4. ✅ Assign the Required Roles
- In the **Service Account Roles** tab, you will see a list of available roles.
- Assign the necessary roles to the service account to allow it to perform actions like user creation, group management, realm and client creation, etc.

### 📜 Mandatory Roles to Assign
- **realm-management**: Allows the service account to manage realms, users, and groups.
- **manage-users**: Grants the ability to create and manage users.
- **manage-clients**: Grants permission to create and manage clients.
- **manage-realm**: Allows management of realm-level settings.

## 5. 💾 Save the Configuration
- After assigning the required roles, click **Save** to apply the changes.

After assigning the necessary roles to the service account, **Feijuca.Auth.Api** will be able to make authenticated requests to Keycloak's API, allowing you to manage users, groups, clients, roles, and more within the realm. Once this is complete, you can proceed to the next step: [🔗 Mandatory Configurations - Feijuca.Auth.Api](https://coderaw-io.github.io/Feijuca.Auth/docs/feijucaMandatoryConfigs.html).
