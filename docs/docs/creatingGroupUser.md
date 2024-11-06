### ⚙️ Endpoint specification  

##### Method: POST
##### Path: /group/user
##### Summary:

Adds a user to the specific group in the specified Keycloak realm.

##### Responses
| Code | Description |
| ---- | ----------- |
| 204 | The user was successfully added to the group. |
| 400 | The request was invalid or could not be processed. |
| 500 | An internal server error occurred during the processing of the request. |
    
##### Header

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| Tenant | header | The tenant identifier used to filter the clients within a specific Keycloak realm. | Yes | string |

##### Body definition

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| userId | body | The user Id related to the user who will be added to the group. | Yes | string |
| groupId | body | The groupId related to the group where the user will be added. | Yes | string |
	
##### Definition
![Endpoint definition](https://res.cloudinary.com/dd7cforjd/image/upload/i0oalcioywksxhbwc1wd.jpg "Endpoint definition")   


### 📝 How to Use the Endpoint

1. **Tenant Identification**:
   - The term *tenant* in Feijuca represents the **realm name** within Keycloak where you’ll be performing actions.
   - You must specify the tenant name in the **HTTP header** to proceed.


2. **Body**:
   - After setting up the header, inform a valid body to insert a user. Example:  

	```json
	{  
	  "userId": "Unique user identifier",
      "groupId": "Unique group identifier"
	}
	
	```
