### ⚙️ Endpoint specification

##### Method: PUT

##### Path: /user

##### Summary:

Updates an existing user in a Keycloak realm.

##### Responses

| Code | Description                                                                                           |
| ---- | ----------------------------------------------------------------------------------------------------- |
| 201  | Authentication was successful, and the JWT token and user attributes was updated.                     |
| 202  | Accepted.                                                                                             |
| 400  | The request was invalid, such as incorrect credentials.                                               |
| 401  | The request lacks valid authentication credentials.                                                   |
| 403  | The request was understood, but the server is refusing to fulfill it due to insufficient permissions. |
| 500  | An internal server error occurred while processing the request.                                       |

##### Header

| Name   | Located in | Description                                                                        | Required | Schema |
| ------ | ---------- | ---------------------------------------------------------------------------------- | -------- | ------ |
| Tenant | header     | The tenant identifier used to filter the clients within a specific Keycloak realm. | Yes      | string |

##### Query Parameter

| Name | Located in | Description                                          | Required | Schema |
| ---- | ---------- | ---------------------------------------------------- | -------- | ------ |
| id   | query      | The user id that should have the attributes updated. | Yes      | Guid   |

##### Body definition

| Name       | Located in | Description                               | Required | Schema |
| ---------- | ---------- | ----------------------------------------- | -------- | ------ |
| username   | body       | The username of the user to be updated.   | Yes      | string |
| password   | body       | The password of the user to be updated.   | Yes      | string |
| email      | body       | The email of the user to be updated.      | Yes      | string |
| firstName  | body       | The first name of the user to be updated. | Yes      | string |
| lastName   | body       | The last name of the user to be updated.  | Yes      | string |
| attributes | body       | The attributes of the user to be updated. | No       | Object |

##### Definition

![Endpoint definition](https://res.cloudinary.com/dd7cforjd/image/upload/rls0cl8yzhhblgcwle4w "Endpoint definition")

### 📝 How to Use the Endpoint

1. **Tenant Identification**:

   - The term _tenant_ in Feijuca represents the **realm name** within Keycloak where you’ll be performing actions.
   - You must specify the tenant name in the **HTTP header** to proceed.

2. **Query Parameter**:

   - Provide the `id` of the user in the query string to specify the user to update.

3. **Body**:

   - After setting up the header and query parameter, provide a valid body to update the user attributes. Example:

   ```json
   {
     "username": "updatedTest",
     "email": "updated@test.com",
     "firstName": "Updated Firstname",
     "lastName": "Updated Lastname",
     "attributes": {
       "updated-attribute": ["new-value"]
     }
   }
   ```

---
