### ⚙️ Endpoint specification  

##### Method: POST
##### Path: /user/logout
##### Summary:

Logs out a user and invalidates their session.

##### Responses
| Code | Description |
| ---- | ----------- |
| 200 | The logout was successful. |
| 400 | The request was invalid or the logout could not be processed. |
| 500 | An internal server error occurred during the processing of the request. |
    
##### Header

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| Tenant | header | The tenant identifier used to filter the clients within a specific Keycloak realm. | Yes | string |

##### Body definition

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| refreshToken | body | The refreshToken is used to identify the user session that will be invalidated. | Yes | string |

	
##### Definition
![Endpoint definition](https://res.cloudinary.com/dd7cforjd/image/upload/clwtzbqpzigks7footg8.jpg "Endpoint definition")   


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
---

### 📝 Reminder


