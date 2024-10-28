## 🔑 Configuring permissions in Keycloak for API Integration

In order for your API to perform operations such as creating users, groups, realms, clients, roles, group roles, and more in Keycloak, you need to configure the appropriate permissions in the realm. This configuration is done by granting specific permissions to the **Service Account** associated with the client your API uses. Follow the steps below:

### 1. 🖥️ Access the Keycloak Admin console
- Log in to the Keycloak Admin Console and select the realm where you want to configure Feijuca.Auth.
---
### 2. 📄 Define or select the client that will represent Feijuca.Auth.
- We recommend you create a new client dedicated to handling the operations.

![Create client example](https://res.cloudinary.com/dbyrluup1/image/upload/sgm76xrjufxskg9dy7ed.jpg "Example of client configuration in Keycloak")

---
### 3. 🔧 Access the **Service Account Roles** Tab
- On the client page, click on the **Service Account Roles** tab to manage the permissions for the service account.

---
### 4. ✅ Assign the Required Roles
- In the **Service Account Roles** tab, you will see a list of available roles.
- Assign the necessary roles to the service account to allow it to perform actions like user creation, group management, realm and client creation, etc.

![Add service role to the client](https://res.cloudinary.com/dbyrluup1/image/upload/tck5z6yhvqhxdq8aciqo.jpg "Add service role to the client")

---
### 📜 Mandatory Roles to Assign
- **After clicking "Assign Role"**, switch the filter to **"Filter by Clients"** to ensure you're assigning the correct roles to the service account.

  Then, assign the following essential realm-management roles to give the service account the necessary permissions:

  - **`realm-admin`**: Grants full administrative access to all realm-level operations and settings.
  
---
### 5. 💾 Save the Configuration
- After finish the assignment of the required roles, the final result should be similar to: 
![Final result](https://res.cloudinary.com/dbyrluup1/image/upload/cguwlrnek8q2fzyyam0j.jpg "Roles services added to the client")

---
### ✅ Successfully Assigned Roles to Service Account

After assigning the necessary roles to the service account, **Feijuca.Auth.Api** will be able to make authenticated requests to Keycloak's API, allowing you to manage users, groups, clients, roles, and more within the realm. 

Once this is complete, you can proceed to the next step: [Configuring the API](/Feijuca.Auth/docs/feijucaMandatoryConfigs.html).
