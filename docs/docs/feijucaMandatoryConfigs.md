### 🚀 Configuration for Keycloak Integration

To take full advantage of the various endpoints provided by **Feijuca.Auth.Api**, a quick configuration is required to store details about the [Previously created client](/Feijuca.Auth/docs/keycloakMandatoryConfigs.html).  
These configurations are crucial. They allow **Feijuca.Auth.Api** to authenticate and retrieve permission tokens to manage the realms.

---

### Configuration - General overview
To use Feijuca.Auth.Api a quickly configuration is required. Basically Feijuca needs authenticate with keycloak every request that you do to generate a jwt token.  
With this token, Feijuca.Auth.Api will be authenticated with your keycloak realm and will be possible do actions related to the realms.
Feijuca needs a connection string to store the client id and secret related to the client that you [created](/Feijuca.Auth/docs/keycloakMandatoryConfigs.html).  .


#### 🛠️ Step 1: Creating a mongodb instance to store data related to the master realm created
💡 **It is important to remember that this instance belongs to you** — we only expect you to define the connection string. **Feijuca.Auth.Api** only uses the data provided to authenticate with Keycloak.  
The connection string you provide allows Feijuca.Auth.Api to securely authenticate with Keycloak, but we store the data into your db provided by a connectionstring.  
All client data is saved directly in your own database. 

To store the data related to the created client previosly we chose **MongoDB** as the data repository, given its flexibility and ease of configuration.  
> **Tip**: If you don't have a MongoDB instance set up, you can create a free mongoDB server on [MongoDB Atlas](https://www.mongodb.com/products/platform/atlas-database).   
> **Wanna change it to your scenario and add support to a new db?**: Feel free to open a **[Pull Request](https://github.com/coderaw-io/Feijuca.Auth/pulls)** to contribute your custom solution! 🚀


---
#### 🐳 Step 2: Running Feijuca.Auth.Api with Docker

Once your MongoDB instance is ready, you can run **Feijuca.Auth.Api** using Docker. You need to pass the MongoDB connection string as an environment variable to ensure the API can communicate with the database.

Run the following command to start the API:

```bash
docker run -e ConnectionString="mongodb://<username>:<password>@<host>:<port>" coderaw/feijuca-auth-api:latest
```

> **Connection string**: Note that you informed the connectionstring into a env variable and it will be used during Feijuca.Auth.Api execution.


---

#### 🛠️ Step 3: Inserting the initial configurations using the api

After accessing the URL where **Feijuca.Auth.Api** is running and appending `/swagger`, you will see all the available API endpoints.  
However, this is only an endpoint definition. 🚧 **Before using these endpoints, if you try used, you will receive an error related to missing config**  
This last configuration involves providing the **Master client id and secret** created in the `master` realm and defining whether the **Feijuca.Auth.Api** should be configured in a **new realm** or an **existing realm**. 

⚠️ **Important Note:**  
Do not confuse the steps! First, create a **master client** to manage all actions within the Keycloak instance.  
Now, in this step, you will create a **client** to manage actions within the **new realm** you define or within an **existing realm**.  
To insert the realm configuration, send an **HTTP POST** request to the `/api/v1/config` endpoint, with the following JSON body:

# [Configuring using existing realm](#tab/existing)
🛠️ If you are already using Keycloak with a configured realm and wish to integrate it with Feijuca.Auth.Api, you'll be glad to know that the process is simple.

🖥️ Endpoint definition
![Endpoint definition](https://res.cloudinary.com/dbyrluup1/image/upload/conqyx0imgwmivjtzkwr "Endpoint definition")

🖥️ Body definition

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| Realm Admin User | body | An email and password for a new user will need to be created within the realm. (Don’t worry, we don’t require access to your Keycloak Admin data). This user will be authorized to authenticate with Feijuca.Auth.Api and perform advanced actions. | Yes | object |
| Master Client | body | The client id from the client that was created on realm master on the previosly steps. | Yes | object |
| Master Client Secret | body | The client secret from the client that was created on realm master on the previosly steps. | Yes | object |
| Server settings | body | The url where you keycloak is running. To consume the keycloak api, Feijuca needs this url. | Yes | object |
| Realm | body | The realm name where the feijuca client will be created. | Yes | object |

🖥️ Body example

```json
{
  "realmAdminUser": {
    "email": "admin@feijuca-auth.com", -- Inform a new user
    "password": "admin" --Inform a password
  },
  "masterClient": {
    "clientId": "feijuca-auth-api" --Client created on master realm
  },
  "masterClientSecret": {
    "clientSecret": "secret-retrieved-from-client" --Client created on master realm
  },
  "serverSettings": {
    "url": "keycloak-url"
  },
  "realm": {
    "name": "your-realm-where-feijuca-client-will-be-created"
  }
}
```

🖥️ Responses

| Code | Description |
| ---- | ----------- |
| 201 | The configuration was successfully inserted. |
| 400 | The request was invalid or could not be processed. |
| 500 | An internal server error occurred during the processing of the request. |

# [Configuring using a new realm](#tab/new)

🛠️ If you are wanna use a new realm to configure Feijuca, the steps is the same, the only difference is that during the configurations a new realm will be created.


🖥️ Endpoint definition
![Endpoint definition](https://res.cloudinary.com/dbyrluup1/image/upload/conqyx0imgwmivjtzkwr "Endpoint definition")

🖥️ Body definition

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| Realm Admin User | body | An email and password for a new user will need to be created within the realm. (Don’t worry, we don’t require access to your Keycloak Admin data). This user will be authorized to authenticate with Feijuca.Auth.Api and perform advanced actions. | Yes | object |
| Master Client | body | The client id from the client that was created on realm master on the previosly steps. | Yes | object |
| Master Client Secret | body | The client secret from the client that was created on realm master on the previosly steps. | Yes | object |
| Server settings | body | The url where you keycloak is running. To consume the keycloak api, Feijuca needs this url. | Yes | object |
| Realm | body | The realm name where the feijuca client will be created. | Yes | object |

🖥️ Body example

```json
{
  "realmAdminUser": {
    "email": "admin@feijuca-auth.com", -- Inform a new user
    "password": "admin" --Inform a password
  },
  "masterClient": {
    "clientId": "feijuca-auth-api" --Client created on master realm
  },
  "masterClientSecret": {
    "clientSecret": "secret-retrieved-from-client" --Client created on master realm
  },
  "serverSettings": {
    "url": "keycloak-url"
  },
  "realm": {
    "name": "your-realm-where-feijuca-client-will-be-created"
  }
}
```

🖥️ Responses

| Code | Description |
| ---- | ----------- |
| 201 | The configuration was successfully inserted. |
| 400 | The request was invalid or could not be processed. |
| 500 | An internal server error occurred during the processing of the request. |

---


#### 🔐✅ Step 4: Reestart the container

After completing the configuration, simply restart your container to apply the changes and enable the project to start with the updated settings.  
Once restarted, you'll have full access to all endpoints and can seamlessly manage the various instances within your Keycloak realm. From there, you can begin managing users, groups, roles, and much more with ease.

### 👨‍🔧 Ready to the next steps? [Handling users](/docs/gettingUsers.html).


