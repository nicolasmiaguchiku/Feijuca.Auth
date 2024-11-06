### ⚙️ Endpoint specification  

##### Method: GET
##### Path: /group/{id}/roles
##### Summary:

Retrivies the roles associated with a specific group in the specified Keycloak realm.

##### Responses
| Code | Description |
| ---- | ----------- |
| 200 | The roles associated with the group were successfully retrieved. |
| 400 | The request was invalid or could not be processed. |

##### Header

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| ID | patch | The unique identifier of the group whose roles are being retrieved. | Yes | Guid |
| Tenant | header | The tenant identifier used to filter the clients within a specific Keycloak realm. | Yes | string |
	
##### Definition
![Endpoint definition](https://res.cloudinary.com/dd7cforjd/image/upload/fvccfnwzmt21herb7jhz.jpg "Endpoint definition")   


### 📝 How to Use the Endpoint

1. **Tenant Identification**:
   - The term *tenant* in Feijuca represents the **realm name** within Keycloak where you’ll be performing actions.
   - You must specify the tenant name in the **HTTP header** to proceed.
2. **Id Unique Identification**:
   - The term *id* represents the **unique identifier of the group** whose roles are being retrieved.
   - You must specify the Group Id in the **HTTP path** to proceed.

