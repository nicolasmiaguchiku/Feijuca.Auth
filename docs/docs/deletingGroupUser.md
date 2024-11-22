### ⚙️ Endpoint specification

##### Method: DELETE

##### Path: /group/user

##### Summary:

Removes a user from a specific group within the specified Keycloak realm.

##### Responses

| Code | Description                                                                                           |
| ---- | ----------------------------------------------------------------------------------------------------- |
| 204  | The user was successfully removed from the group.                                                     |
| 400  | The request was invalid or could not be processed.                                                    |
| 401  | The request lacks valid authentication credentials.                                                   |
| 403  | The request was understood, but the server is refusing to fulfill it due to insufficient permissions. |
| 500  | An internal server error occurred during the processing of the request.                               |

##### Header

| Name   | Located in | Description                                                                        | Required | Schema |
| ------ | ---------- | ---------------------------------------------------------------------------------- | -------- | ------ |
| Tenant | header     | The tenant identifier used to filter the clients within a specific Keycloak realm. | Yes      | string |

##### Body definition

| Name    | Located in | Description                                                           | Required | Schema |
| ------- | ---------- | --------------------------------------------------------------------- | -------- | ------ |
| userId  | body       | The userId related to the user that will be deleted.                  | Yes      | Guid   |
| groupId | body       | The groupId related to the group from which the user will be deleted. | Yes      | Guid   |

##### Definition

![Endpoint definition](https://res.cloudinary.com/dd7cforjd/image/upload/yjjjdfs8mfp0sfqnozu8.jpg "Endpoint definition")

### 📝 How to Use the Endpoint

1. **Tenant Identification**:

   - The term _tenant_ in Feijuca represents the **realm name** within Keycloak where you’ll be performing actions.
   - You must specify the tenant name in the **HTTP header** to proceed.

2. **Body**:

   - After setting up the header, inform a valid body to insert a user. Example:

   ```json
   {
     "userId": "Unique user identifier",
     "groupId": "Unique group identifier"
   }
   ```
