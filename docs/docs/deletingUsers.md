### ⚙️ Endpoint specification  

##### Method: DELETE
##### Path: /user
##### Summary:

Deletes an existing user from the specified Keycloak realm.


##### Responses
| Code | Description |
| ---- | ----------- |
| 204 | The user was deleted successfully. |
| 400 | The request was invalid or the user could not be found. |
| 401 | The request lacks valid authentication credentials. |
| 403 | The request was understood, but the server is refusing to fulfill it due to insufficient permissions. |
| 500 | An internal server error occurred during the processing of the request. |
    
##### Header

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| Tenant | header | The tenant identifier used to filter the clients within a specific Keycloak realm. | Yes | string |

##### Query params

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| id | query | The unique identifier of the user to be deleted. | Yes | Guid |

##### Definition
![Endpoint definition](https://res.cloudinary.com/dbyrluup1/image/upload/cjebswgv8tgttk6fl8ld.jpg "Endpoint definition")   


### 📝 How to Use the Endpoint

1. **Tenant Identification**:
   - The term *tenant* in Feijuca represents the **realm name** within Keycloak where you’ll be performing actions.
   - You must specify the tenant name in the **HTTP header** to proceed.


2. **Query Parameters**:
   - After setting up the header, use the available **query parameters** to inform what user you want to delete.