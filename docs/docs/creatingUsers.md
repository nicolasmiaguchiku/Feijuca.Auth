### ⚙️ Endpoint specification  

##### POST
##### /user
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
| Username | body | The username related to the new user that will be created. | Yes | string |
| Password | body | The password related to the new user that will be created. | Yes | string |
| Email | body | The e-mail related to the new user that will be created. | Yes | string |
| FirstName | body | The first name related to the new user that will be created.| Yes | string |
| LastName | body | The last name related to the new user that will be created.| Yes | string |
| Attributes | body | The attributes that will be inserted on the new user that was created| No | Object |

#### Body example

```json
{  
  "username": "string",
  "password": "string",
  "email": "string",
  "firstName": "string",
  "lastName": "string",
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
