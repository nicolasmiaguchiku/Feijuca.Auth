### ⚙️ Endpoint specification  


##### Method: GET 
##### Path: /group/user
##### Summary:

Retrieves all users present in the specific group int the specified Keycloak realm.


##### Responses
| Code | Description |
| ---- | ----------- |
| 200 | The users in the group were successfully retrieved. |
| 400 | The request was invalid or could not be processed. |
| 500 | An internal server error occurred during the processing of the request. |

##### Header

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| Tenant | header | The tenant identifier used to filter the clients within a specific Keycloak realm. | Yes | string |
   
##### Query params

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| PageFilter.Page | query | The page related to the data that is being showed. | No | int |
| PageFilter.PageSize | query | Defines the page size, i.e. the maximum number of users that will be returned per page. | No | int |
| GroupId | query | The group ids that can be used to filter determinated groups. | No | Guid |
| Emails | query | Allows you to filter users based on a list of email addresses. | No | string |


##### Definition
![Endpoint definition](https://res.cloudinary.com/dd7cforjd/image/upload/xvsup3z5h4xsmxxw2tgt.jpg "Endpoint definition")   


### 📝 How to Use the Endpoint

1. **Tenant Identification**:
   - The term *tenant* in Feijuca represents the **realm name** within Keycloak where you’ll be performing actions.
   - You must specify the tenant name in the **HTTP header** to proceed.


2. **Query Parameters**:
   - After setting up the header, use the available **query parameters** to customize your search based on your requirements.

---

### 🔍 Available Search Types

#### 1. **Get All Groups**  
   - Fetches all users within the realm.
   - The results are provided in a paginated format, allowing you to navigate through pages to explore the user list.

#### 2. **Get Group by ID**
   - Enables filtering by **Group IDs**.
   - This will search the realm and return the group that matches the given ID.

#### 3. **Get Users by Emails**  
   - Allows filtering by an **array of Emails**.
   - The API will find and return users that match the user emails provided in the request.

---

