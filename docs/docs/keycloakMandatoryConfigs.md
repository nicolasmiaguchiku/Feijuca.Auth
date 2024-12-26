### 🔑 Configuring permissions in Keycloak for API Integration

In order for your API to perform operations such as creating users, groups, realms, clients, roles, group roles, and more in Keycloak, you need to configure the appropriate permissions in the realm. This configuration is done by granting specific permissions to the **Service Account** associated with the client your API uses. Follow the steps below:

### 1. 🖥️ Access the Keycloak Admin console
- Log in to the Keycloak Admin Console and select the master realm and click on Clients. We will create a new client that will be used by the Feijuca.Auth.Api.
---
### 2. 🔧 Into your master realm, create a new client that will represent Feijuca.Auth.

![Create client example](https://res.cloudinary.com/dbyrluup1/image/upload/sgm76xrjufxskg9dy7ed.jpg "Example of client configuration in Keycloak")

---
### 3. 🔧 Define the Capability config
- Let your configurations equals the configs below:
![Create client example](https://res.cloudinary.com/dbyrluup1/image/upload/gux3vn8hvdhhod0roghb "Example of client configuration in Keycloak")

---
### 4. 🔧 At login settings, let it empty, future we will back at this config to add values here.

---

### 5. 🔧 Providing permissions
- In the **Service Account Roles** tab click on Assign role
- Assign the necessary roles to the service account to allow it to perform actions like user creation, group management, realm and client creation, etc.

![Add service role to the client](https://res.cloudinary.com/dbyrluup1/image/upload/vlsnxeyrqqtcctood0ve "Add service role to the client")

---
### ✅ Successfully Assigned Roles to Service Account

After assigning the necessary roles to the service account, **Feijuca.Auth.Api** will be able to make authenticated requests to Keycloak's API, allowing you to manage users, groups, clients, roles, and more within the realm. 

Once this is complete, you can proceed to the next step: [Configuring the API](/Feijuca.Auth/docs/feijucaMandatoryConfigs.html).