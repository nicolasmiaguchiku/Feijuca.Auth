### ⚙️ Endpoint specification  

##### POST
##### Summary:

Add a new user to the specified Keycloak realm. (**Remind that the realm is identified by the tenant header**)

##### Responses
| Code | Description |
| ---- | ----------- |
| 201 | The operation was successful, and the new user was created. |
| 400 | The request was invalid or could not be processed. |
| 401 | The request lacks valid authentication credentials. |
| 403 | The request was understood, but the server is refusing to fulfill it due to insufficient permissions. |
| 500 | An internal server error occurred during the processing of the request. |

   
   
##### Header

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| Tenant | header | The tenant identifier used to filter the clients within a specific Keycloak realm. | Yes | string |

##### Body definition

| Name | Located in | Description | Required | Schema |
| ---- | ---------- | ----------- | -------- | ---- |
| Client | body | The client id related to the client that was created to manage operations in the realm. | Yes | string |
| Secret | body | The client secret related to the client that was created to manage operations in the realm. | Yes | string |
| Server settings | body | The url where you keycloak is running. | Yes | string |
| Realm | body | The realm name (if you wanna use multitenancy concept, inform an array of this object), the audience name configured previosly and the issuer| Yes | object |

#### Body example

```json
	{  
	  "username": "test",
	  "password": "123456",
	  "email": "test@test.com",
	  "firstName": "Test Firstname",
	  "lastName": "Test Lastname",
	  "attributes": {
		  "additionalProp1": [
	          "string"
          ],
		  "additionalProp1": [
	          "string"
          ],
		  "additionalProp1": [
	          "string"
          ]    
	   }
	}
	```
	
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
	  "username": "test",
	  "password": "123456",
	  "email": "test@test.com",
	  "firstName": "Test Firstname",
	  "lastName": "Test Lastname",
	  "attributes": {
		"tenant": [
			"smartconsig"
			]
		}
	}
	
	```
---

### 📝 Reminder

The attributes property is not mandatory, but if you wanna configure some attribute to your user, you can defined it using this field.
An example about how attribute works, could be found below:

![Attributes definition](https://res.cloudinary.com/dbyrluup1/image/upload/e2l4wkakcb1rgrwcxrfp.jpg "Attributes definition")   

The JSON used to created this attribute was:

```json
"attributes": {
    "tenant": [
      "smartconsig"
    ]
  }
}
```
