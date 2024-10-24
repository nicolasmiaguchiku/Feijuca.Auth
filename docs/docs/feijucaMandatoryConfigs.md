# 🚀 Feijuca.Auth.Api Configuration for Keycloak Integration

To take full advantage of the various endpoints provided by **Feijuca.Auth.Api**, a quick configuration is required to input details about your Keycloak realm. These configurations are crucial because they allow **Feijuca.Auth.Api** to authenticate and retrieve permission tokens to manage users, groups, roles, and much more.

---

### Configuration - General overview

**Feijuca.Auth.Api** needs some information about the realm you created in **Keycloak** in the previous step.

To store this realm information, we chose **MongoDB** as the data repository, given its flexibility and ease of configuration.

💡 **It is important to remember that this instance belongs to you** — we only expect you to define the connection string. **Feijuca.Auth.Api** only uses the data provided to authenticate with Keycloak. 🔐

### Contributing with a Different Database

However, if you want to extend the project and use a different database, feel free to open a **[Pull Request](https://github.com/coderaw-io/Feijuca.Auth/pulls)** to contribute your custom solution! 🚀

---

## ⚙️ Step 1: Setting up mongoDB connection string

The first configuration you need to provide is the **MongoDB connection string**. This will enable you to store and manage the Keycloak realm settings.

> **Tip**: If you don't have a MongoDB instance set up, you can create a free mongoDB server on [MongoDB Atlas](https://www.mongodb.com/products/platform/atlas-database).

---
## 🐳 Step 2: Running Feijuca.Auth.Api with Docker

Once your MongoDB instance is ready, you can run **Feijuca.Auth.Api** using Docker. You need to pass the MongoDB connection string as an environment variable to ensure the API can communicate with the database.

Run the following command to start the API:

```bash
docker run -e ConnectionString="mongodb://<username>:<password>@<host>:<port>" coderaw/feijuca-auth-api:latest
```

---

## 🛠️ Step 3: Inserting the realm configuration

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

## Every configs necessary thing is done! 🔐✅

After completing the configuration, you’ll be ready to access all endpoints and easily manage the various instances a Keycloak realm offers. You can now begin managing users, groups, roles, and more.

## 👨‍🔧 Ready to the next steps? [Creating users](/docs/creatingUsers.html).


