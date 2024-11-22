### ⚙️ Endpoint specification

##### Method: POST

##### Path: /user/login

##### Summary:

Decodes the JWT token and returns the details related to the user logged.

##### Responses

| Code | Description                                                                                           |
| ---- | ----------------------------------------------------------------------------------------------------- |
| 200  | The token was successfully decoded, returning user details.                                           |
| 400  | The request was invalid due to an issue with the token or user authentication.                        |
| 401  | The request lacks valid authentication credentials.                                                   |
| 403  | The request was understood, but the server is refusing to fulfill it due to insufficient permissions. |
| 500  | An internal server error occurred during the processing of the request.                               |

##### Header

| Name   | Located in | Description                                                                        | Required | Schema |
| ------ | ---------- | ---------------------------------------------------------------------------------- | -------- | ------ |
| Tenant | header     | The tenant identifier used to filter the clients within a specific Keycloak realm. | Yes      | string |

##### Definition

![Endpoint definition](https://res.cloudinary.com/dd7cforjd/image/upload/bktmamzdgyqvubyv6xmv.jpg "Endpoint definition")

### 📝 How to Use the Endpoint

1. **Tenant Identification**:
   - The term _tenant_ in Feijuca represents the **realm name** within Keycloak where you’ll be performing actions.
   - You must specify the tenant name in the **HTTP header** to proceed.
