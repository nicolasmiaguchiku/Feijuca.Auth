### 🚀 Configuration for Keycloak Integration

To take full advantage of the various endpoints provided by **Feijuca.Auth.Api**, a quick configuration is required to store the details of the [previously created Keycloak client](/Feijuca.Auth/docs/keycloakMandatoryConfigs.html).  
These configurations are essential — they allow **Feijuca.Auth.Api** to authenticate and retrieve permission tokens to manage Keycloak realms.

---

### 🔧 General Overview

To function properly, **Feijuca.Auth.Api** requires some basic configuration.  
Every request to the API triggers an authentication process with Keycloak to generate a JWT token.  
This token allows **Feijuca.Auth.Api** to perform operations related to your Keycloak realm.

Additionally, Feijuca needs a connection string to access a database where it stores the client ID and secret associated with the Keycloak client you [previously created](/Feijuca.Auth/docs/keycloakMandatoryConfigs.html).

---

### 🛠️ Step 1: Set Up a MongoDB Instance

💡 **Important:** The MongoDB instance is entirely yours — we do not host or manage it.  
You are responsible for defining the connection string. **Feijuca.Auth.Api** uses this only to store credentials required to authenticate with Keycloak.  
All data is stored directly in the database provided by you.

We use **MongoDB** for its simplicity and flexibility in document storage.

> **Tip:** If you don’t have a MongoDB instance yet, you can create one for free using [MongoDB Atlas](https://www.mongodb.com/products/platform/atlas-database).  
> **Want to use another database?** Feel free to open a **[Pull Request](https://github.com/coderaw-io/Feijuca.Auth/pulls)** and contribute your custom implementation! 🚀

---

#### 🐳 Step 2: Running Feijuca.Auth.Api with Docker

Once your MongoDB instance is ready, you can run **Feijuca.Auth.Api** using Docker.  
You need to provide the MongoDB connection string **and** the name of the database where Feijuca will store and retrieve data.

Use the following command to start the API:

```bash
docker run \
  -e ConnectionString="mongodb://<username>:<password>@<host>:<port>" \
  -e DatabaseName="feijuca-auth" \
  coderaw/feijuca-auth-api:latest
```

> **ConnectionString**: The `ConnectionString` variable defines how to connect to your MongoDB instance.  
> **DatabaseName**: The `DatabaseName` variable specifies which database inside your MongoDB server will be used to store client credentials configured previosly.

---

#### 🛠️ Step 3: Inserting the Initial Configurations Using the API

After accessing the URL where **Feijuca.Auth.Api** is running and appending `/swagger` or `/scalar`, you will see all the available API endpoints.  
However, on this moment these are only definitions of the endpoints. 

🚧 **Before you can use them, if you try to make a request now, you'll receive an error due to missing configuration.**

This final configuration step involves providing the **Master client ID and secret** created in the `master` realm, and defining whether **Feijuca.Auth.Api** should be configured on a **new realm** or an **existing one**.

To insert this realm configuration, send an **HTTP POST** request to the `/api/v1/config` endpoint with the following JSON body:

#### 🛠️ Step 4: Inserting the configurations

> **Existing realm:** If you already have a realm and want to configure Feijuca there, no problem — it’s fully supported. Use configs/existing-realm  to do that.
> **New realm:** If you want to create a new realm, you can do that as well. Use configs/new-realm  to do that.


🖥️ Body example

```bash
curl https://localhost:7018/api/v1/configs/new-realm \
  --request POST \
  --header 'Content-Type: application/json' \
  --data '{
  "realmAdminUser": {
    "email": "", --Feel free to inform a new user and password
    "password": ""
  },
  "client": {
    "clientId": "feijuca-auth-api" --Do not change it, it is the name of the client created on master realm
  },
  "clientSecret": {
    "clientSecret": "secret-retrieved-from-client"  --Change it, copy and paste the client secret created on master realm 
  },
  "serverSettings": {
    "url": "" --Here you have to put the URL where your Keycloak instance is running, e.g. https://localhost:port
  },
  "realm": {
    "name": -- Choose a new realm name or set an existing one
  }
}'

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


