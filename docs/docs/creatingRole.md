### ⚙️ Endpoint specification  

##### Method: POST
##### Path: /role
##### Summary:

Adds a new role to the client in the specified Keycloak realm.

##### Responses
| Code | Description |
| ---- | ----------- |
| 201 | The operation was successful, and the new role was created. |
| 400 | The request was invalid or could not be processed. |
| 500 | An internal server error occurred during the processing of the request. |
    
##### Header

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| Tenant | header | The tenant identifier used to filter the clients within a specific Keycloak realm. | Yes | string |

##### Body definition

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| clientId | body | The clientId related to the unique identifier of the client that will have the new rule added. | Yes | Guid |
| name | body | The name is related to the rule name. | Yes | string |
| description | body | The description is related to information for that rule. | Yes | string |
	
##### Definition
![Endpoint definition](https://res.cloudinary.com/dd7cforjd/image/upload/xdcrpq1fi3pbx3i2aq1u.jpg "Endpoint definition")   


### 📝 How to Use the Endpoint

1. **Tenant Identification**:
   - The term *tenant* in Feijuca represents the **realm name** within Keycloak where you’ll be performing actions.
   - You must specify the tenant name in the **HTTP header** to proceed.


2. **Body**:
   - After setting up the header, inform a valid body to insert a user. Example:  

	```json
	{  
	  "clientId": "Unique identifier of your customer(Guid)",
      "name": "test",
      "description": "description test"
	}
	
	```
