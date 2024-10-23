# 🚀 Feijuca.Auth.Api Configuration for Keycloak Integration

To take full advantage of the various endpoints provided by **Feijuca.Auth.Api**, a quick configuration is required to input details about your Keycloak realm. These configurations are crucial because they allow **Feijuca.Auth.Api** to authenticate and retrieve permission tokens to manage users, groups, roles, and much more.

---

## ⚙️ Step 1: Setting up mongoDB connection string

The first configuration you need to provide is the **MongoDB connection string**. This will enable you to store and manage the Keycloak realm settings.

Since **Feijuca.Auth.Api** is Docker-supported, we suggest pulling the Docker image and defining the connection URL using an environment variable. Run the following command:

```bash
docker run -e ConnectionString="mongodb://admin:mysecretpassword@mongodb.local:27017/mydatabase" fmattioli/feijuca-tokenmanager-api:latest
```

> **Tip**: If you don't have a MongoDB instance set up, you can create a free MongoDB server on [MongoDB Atlas](https://www.mongodb.com/products/platform/atlas-database) with some limitations in terms of storage.

## 📂 Why MongoDB?

We chose MongoDB as the initial data repository for **Feijuca.Auth.Api** to store the realm configurations, given its flexibility and ease of setup.

However, if you want to extend the project and use a different database, feel free to open a **Pull Request (PR)** to contribute your custom solution!

## 🛠️ Step 2: Inserting the Realm Configuration

Once your Docker container is up and running with the correct configuration, you're ready to insert your Keycloak realm configuration.

To do this, send an **HTTP POST** request to the `/api/v1/config` endpoint, with the following JSON body:

```json
{
  "clientId": "string",
  "clientSecret": "string",
  "authServerUrl": "string",
  "realms": [
    {
      "name": "string",
      "audience": "string",
      "issuer": "string",
      "useAsDefaultSwaggerTokenGeneration": true
    }
  ],
  "policyName": "string",
  "roles": [
    "string"
  ],
  "scopes": [
    "string"
  ]
}
```

## 🔐 Step 3: Using the API

After completing the configuration, you’ll be ready to access all endpoints and easily manage the various instances a Keycloak realm offers. You can now begin managing users, groups, roles, and more.

## 🚧 Next Step: Creating Users

Follow the next steps to create users and fully manage your Keycloak realm configurations using **Feijuca.Auth.Api**.
