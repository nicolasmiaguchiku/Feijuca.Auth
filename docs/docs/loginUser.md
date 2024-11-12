   ### ⚙️ Endpoint specification  

##### Method: POST
##### Path: /user/login
##### Summary:

Authenticates a user and returns a valid JWT token along with user details.

##### Responses
| Code | Description |
| ---- | ----------- |
| 200 | Authentication was successful, and the JWT token and user details are returned. |
| 400 | The request was invalid, such as incorrect credentials. |
| 500 | An internal server error occurred while processing the request. |
    
##### Header

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| Tenant | header | The tenant identifier used to filter the clients within a specific Keycloak realm. | Yes | string |

##### Body definition

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| Username | body | The username related to the new user that will be created. | Yes | string |
| Password | body | The password related to the new user that will be created. | Yes | string |
| revokeActiveSessions | body | A boolean parameter (true or false) indicating whether the user's active sessions should be revoked during the login process.  | Yes | string |
	
##### Definition
![Endpoint definition](https://res.cloudinary.com/dd7cforjd/image/upload/grjqydunfwsnn1ffny5e.jpg "Endpoint definition")   


### 📝 How to Use the Endpoint

1. **Tenant Identification**:
   - The term *tenant* in Feijuca represents the **realm name** within Keycloak where you’ll be performing actions.
   - You must specify the tenant name in the **HTTP header** to proceed.


2. **Body**:
   - After setting up the header, inform a valid body to insert a user. Example:  

	```json
	{  
	  "username": "teste@teste.com",
      "password": "teste12345",
      "revokeActiveSessions": true
	}
	
	```