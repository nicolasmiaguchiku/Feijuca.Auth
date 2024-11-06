### ⚙️ Endpoint specification  

##### Method: POST
##### Path: /group/{id}/role
##### Summary:

Adds a role to a specific group in the specified Keycloak realm.

##### Responses
| Code | Description |
| ---- | ----------- |
| 201 | The role was successfully added to the group. |
| 400 | The request was invalid or could not be processed. |

##### Header

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ------ |
| ID | patch | The unique identifier of the group to which the role will be added. | Yes | Guid |
| Tenant | header | The tenant identifier used to filter the clients within a specific Keycloak realm. | Yes | string |


##### Body definition

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| clientId  | body  | The clientId related to the unique identifier of the client that will have the new rule added. | Yes   | Guid        |
| roleId    | body  | The roleId related to the unique identifier of the rule to be added to the Group. | Yes   | Guid        |
	
##### Definition
![Endpoint definition](https://res.cloudinary.com/dd7cforjd/image/upload/szxkceaew0arzygn7p1z.jpg "Endpoint definition")   


### 📝 How to Use the Endpoint

1. **Tenant Identification**:
   - The term *id* represents the **realm name** within Keycloak where you’ll be performing actions.
   - You must specify the tenant name in the **HTTP header** to proceed.
2. **Id Unique Identification**:
   - The term *id* in Feijuca represents the **unique identifier of the group** within Keycloak where you will execute the actions.
   - You must specify the Group Id in the **HTTP path** to proceed.


2. **Body**:
   - After setting up the header, inform a valid body to insert a user. Example:  

	```json
	{  
	  "clientId": "Unique client identifier",
      "roleId": "Unique role identifier"
	}
	
	```
