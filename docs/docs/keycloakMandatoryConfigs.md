### 🔑 Setting a master client to allow Keycloak Integration

In order to perform operations such as creating users, groups, realms, clients, roles, group roles, and more using Feijuca.Auth.Api, you need to configure a new client in the realm master. Follow the steps below:

### 1. 🖥️ Access the Keycloak Admin console
- Log in to the Keycloak Admin Console and select the master realm and click on Clients. We will create a new client that will be used by the Feijuca.Auth.Api.
---
### 2. 🔧 Into your master realm, create a new client that will represent Feijuca.Auth. (Please, use the same values as you see below and remember to do it on master realm.)

![Create client example](https://res.cloudinary.com/dbyrluup1/image/upload/sgm76xrjufxskg9dy7ed.jpg "Example of client configuration in Keycloak")

---
### 3. 🔧 Define the Capability config
- Let your configurations equals the configs below:
![Create client example](https://res.cloudinary.com/dbyrluup1/image/upload/gux3vn8hvdhhod0roghb "Example of client configuration in Keycloak")

---
### 4. 🔧 At login settings, let it as it is and proceed creating the client.

---

### 5. 🔧 Assigning permissions"
- After create the client, click on them and go to **Service Account Roles** tab.
- In the **Service Account Roles** tab click on Assign role.
- Assign the necessary admin role to the client to allow it to perform actions like user creation, group management, realm and client creation, etc.

![Add service role to the client](https://res.cloudinary.com/dbyrluup1/image/upload/vlsnxeyrqqtcctood0ve "Add service role to the client")

---
### ✅ Successfully Assigned Roles to Service Account

After assigning the necessary roles to the service account, **Feijuca.Auth.Api** will be able to make authenticated requests to Keycloak's API, allowing you to manage and handle users, groups, clients, roles, realms and more...

Once this is complete, you can proceed to the next step: [Configuring the Feijuca.Auth.Api](/Feijuca.Auth/docs/feijucaMandatoryConfigs.html).