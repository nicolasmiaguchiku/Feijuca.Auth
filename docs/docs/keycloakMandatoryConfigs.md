# Configuring Permissions in Keycloak for API Integration

In order for your API to perform operations such as creating users, groups, realms, clients, roles, group roles, and more in Keycloak, you need to configure the appropriate permissions in the realm. 
This configuration is done by granting specific permissions to the **Service Account** associated with the client your API uses. Follow the steps below:

## 1. Access the Keycloak Admin Console
- Log in to the Keycloak Admin Console and select the realm where you want to configure permissions.

## 2. Define or Select the Client will represent your API
- We recommend you create a new client dedicated to handle the operations.

## 3. Access the `Service Account Roles` Tab
- On the client page, click on the **Service Account Roles** tab to manage the permissions for the service account.

## 4. Assign the Required Roles
- In the **Service Account Roles** tab, you will see a list of available roles.
- Assign the necessary roles to the service account to allow it to perform actions like user creation, group management, realm and client creation, etc.

### Mandatory Roles to Assign
- **realm-management**: Allows the service account to manage realms, users, and groups.
- **manage-users**: Grants the ability to create and manage users.
- **manage-clients**: Grants permission to create and manage clients.
- **manage-realm**: Allows management of realm-level settings.

## 5. Save the Configuration
- After assigning the required roles, click **Save** to apply the changes.

Once the appropriate roles are assigned to the service account, your API will be able to make authenticated calls to Keycloak's API to manage users, groups, clients, roles, and more within the realm.
Once it is done, you can go to next step: (Feijuca.Auth.Api mandatory configs)[/feijucaMandatoryConfigs].
