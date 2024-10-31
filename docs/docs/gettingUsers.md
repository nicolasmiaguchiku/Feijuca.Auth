### ⚙️ Endpoint specification  

##### GET 
##### /users
##### Summary:

Retrieves all users existing in the specified Keycloak realm.


##### Responses
| Code | Description |
| ---- | ----------- |
| 200 | The operation was successful, and the configuration settings are returned. |
| 400 | The request was invalid or could not be processed. |
| 500 | An internal server error occurred during the processing of the request. |
| 401 | The request lacks valid authentication credentials. |
| 403 | The request was understood, but the server is refusing to fulfill it due to insufficient permissions. |
   

##### Header

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| Tenant | header | The tenant identifier used to filter the clients within a specific Keycloak realm. | Yes | string |
   
##### Query params

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| PageFilter.Page | query | The page related to the data that is being showed. | No | int |
| Ids | query | The user ids that can be used to filter determinated users. | No | Guid |
| Usernames | query | The Usernames that can be used to filter determinated users. | No | int |


##### Definition
![Endpoint definition](https://res.cloudinary.com/dbyrluup1/image/upload/g5hp8rn1fpefqc101iqs.jpg "Endpoint definition")   


### 📝 How to Use the Endpoint

1. **Tenant Identification**:
   - The term *tenant* in Feijuca represents the **realm name** within Keycloak where you’ll be performing actions.
   - You must specify the tenant name in the **HTTP header** to proceed.


2. **Query Parameters**:
   - After setting up the header, use the available **query parameters** to customize your search based on your requirements.

---

### 🔍 Available Search Types

#### 1. **Get All Users**  
   - Fetches all users within the realm.
   - The results are provided in a paginated format, allowing you to navigate through pages to explore the user list.

#### 2. **Get Users by ID**  
   - Enables filtering by an **array of user IDs**.
   - This will search the realm and return users matching the provided IDs.

#### 3. **Get Users by Username**  
   - Allows filtering by an **array of usernames**.
   - The API will locate and return users that match the usernames provided in the request.

---

