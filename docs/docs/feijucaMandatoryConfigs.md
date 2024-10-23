# 🚀 Feijuca.Auth.Api Configuration for Keycloak Integration

To take full advantage of the various endpoints provided by **Feijuca.Auth.Api**, a quick configuration is required to input details about your Keycloak realm. These configurations are crucial because they allow **Feijuca.Auth.Api** to authenticate and retrieve permission tokens to manage users, groups, roles, and much more.

---

## ⚙️ Step 1: Setting up mongoDB connection string

The first configuration you need to provide is the **MongoDB connection string**. This will enable you to store and manage the Keycloak realm settings.

Since **Feijuca.Auth.Api** is Docker-supported, we suggest pulling the Docker image and defining the connection URL using an environment variable. Run the following command:

```bash
docker run -e ConnectionString="mongodb://admin:mysecretpassword@mongodb.local:27017/mydatabase" coderaw/feijuca-auth-api:latest
```

> **Tip**: If you don't have a MongoDB instance set up, you can create a free mongoDB server on [MongoDB Atlas](https://www.mongodb.com/products/platform/atlas-database).

## 📂 Why Feijuca.Auth.Api use mongoDB?

We chose MongoDB as the initial data repository for to store the realm configurations, given its flexibility and ease of setup. We use a database only for store the realm config that is necessary to authenticate with Keycloak.
Is good remember that this instance belongs to you, you have only set the connection string. Feijuca.Auth has no access directly to your data!

However, if you want to extend the project and use a different database, feel free to open a **Pull Request (PR)** to contribute your custom solution!

## 🛠️ Step 2: Inserting the realm configuration

Once your Docker container is up and running with the correct configuration, you're ready to insert your Keycloak realm configuration.

To do this, send an **HTTP POST** request to the `/api/v1/config` endpoint, with the following JSON body:

```json
{  
  "client": {
    "clientId": "your-client-id"
  },
  "secrets": {
    "clientSecret": "your-client-secret"
  },
  "serverSettings": {
    "url": "your-keycloak-url (from example https://localhost:8079)"
  },
  "realm": {
    "name": "realm-name",
    "audience": "audience-name",
    "issuer": "same-url-defined-above/realms/realm-name-defined-above"
  }
}
```

## 🔐 Step 3: Using the API

After completing the configuration, you’ll be ready to access all endpoints and easily manage the various instances a Keycloak realm offers. You can now begin managing users, groups, roles, and more.

## 🚧 Next step: Creating users [here](/docs/creatingUsers.html).


