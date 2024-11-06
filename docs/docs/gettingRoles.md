### ⚙️ Endpoint specification  

##### Method: GET
##### Path: /roles
##### Summary:

Retrieves all roles associated with clients in the specified Keycloak realm.

##### Responses
| Code | Description |
| ---- | ----------- |
| 200 | A list of roles associated with the clients in the specified realm |
| 400 | The request was invalid or could not be processed. |
| 500 | An internal server error occurred during the processing of the request. |
    
##### Header

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| Tenant | header | The tenant identifier used to filter the clients within a specific Keycloak realm. | Yes | string |
	
##### Definition
![Endpoint definition](https://res.cloudinary.com/dd7cforjd/image/upload/s0wp770hduegt2qxnomx.jpg "Endpoint definition")   


### 📝 How to Use the Endpoint

1. **Tenant Identification**:
   - The term *tenant* in Feijuca represents the **realm name** within Keycloak where you’ll be performing actions.
   - You must specify the tenant name in the **HTTP header** to proceed.

