### ⚙️ Endpoint specification  

##### Method: POST
##### Path: /user
##### Summary:

Recover all clients registered in the realm.

##### Responses
| Code | Description |
| ---- | ----------- |
| 200 | The operation was successful, and the list of clients is returned. |
| 400 | The request was invalid or could not be processed. |
| 500 | An internal server error occurred during the processing of the request. |
    
##### Header

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| Tenant | header | The tenant identifier used to filter the clients within a specific Keycloak realm. | Yes | string |
	
##### Definition
![Endpoint definition](https://res.cloudinary.com/dd7cforjd/image/upload/j8srijrpgoh9ztyni5n8.jpg "Endpoint definition")   


### 📝 How to Use the Endpoint

1. **Tenant Identification**:
   - The term *tenant* in Feijuca represents the **realm name** within Keycloak where you’ll be performing actions.
   - You must specify the tenant name in the **HTTP header** to proceed.

