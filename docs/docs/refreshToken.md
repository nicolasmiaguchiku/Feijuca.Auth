### ⚙️ Endpoint specification  

##### Method: POST
##### Path: /user/refresh-token
##### Summary:

Refreshes a valid JWT token and returns the new token along with user details.

##### Responses
| Code | Description |
| ---- | ----------- |
| 200 | The refresh operation was successful, returning a new token and user details. |
| 400 | The request was invalid due to an issue with the refresh token. |
| 500 | An internal server error occurred while processing the request. |
    
##### Header

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| Tenant | header | The tenant identifier used to filter the clients within a specific Keycloak realm. | Yes | string |

##### Body definition

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| refreshToken | body | The refreshToken is used to identify the user session that will be invalidated. | Yes | string |
	
##### Definition
![Endpoint definition](https://res.cloudinary.com/dbyrluup1/image/upload/qaa8tdwzt3ub4vkrcvbc.jpg "Endpoint definition")   


### 📝 How to Use the Endpoint

1. **Tenant Identification**:
   - The term *tenant* in Feijuca represents the **realm name** within Keycloak where you’ll be performing actions.
   - You must specify the tenant name in the **HTTP header** to proceed.


2. **Body**:
   - After setting up the header, inform a valid body to insert a user. Example:  

	```json
	{  
	  "refreshToken": "your-refresh-token"
	}
	
	```
