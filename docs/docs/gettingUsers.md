### 🌐 User Search Endpoint

The **Feijuca.Auth API** offers an endpoint for searching users within a Keycloak realm. Below are the key details and instructions for using it effectively:

---

## 📝 How to Use the Endpoint

1. **Tenant Identification**:
   - The term *tenant* in Feijuca represents the **realm name** within Keycloak where you’ll be performing actions.
   - You must specify the tenant name in the **HTTP header** to proceed.

2. **Query Parameters**:
   - After setting up the header, use the available **query parameters** to customize your search based on your requirements.

---

## 🔍 Available Search Types

### 1. **Get All Users**  
   - Fetches all users within the realm.
   - The results are provided in a paginated format, allowing you to navigate through pages to explore the user list.

### 2. **Get Users by ID**  
   - Enables filtering by an **array of user IDs**.
   - This will search the realm and return users matching the provided IDs.

### 3. **Get Users by Username**  
   - Allows filtering by an **array of usernames**.
   - The API will locate and return users that match the usernames provided in the request.

---

## ⚙️ Optional Configurations

- **Page Size**:
   - Define the number of results returned per page by adjusting the **page size** parameter.
   - This setting lets you control the number of items displayed on each page for improved navigation.

---
