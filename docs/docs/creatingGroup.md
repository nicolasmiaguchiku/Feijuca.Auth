### ⚙️ Endpoint specification  

##### Method: POST
##### Path: /group
##### Summary:

Adds a new group to the specified Keycloak realm.

##### Responses
| Code | Description |
| ---- | ----------- |
| 201 | The group was successfully created. |
| 400 | The request was invalid or could not be processed. |
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
	
##### Definition
![Endpoint definition](https://res.cloudinary.com/dd7cforjd/image/upload/cluh2nidozrkw1fgwnpl.jpg "Endpoint definition")   


### 📝 How to Use the Endpoint

1. **Tenant Identification**:
   - The term *tenant* in Feijuca represents the **realm name** within Keycloak where you’ll be performing actions.
   - You must specify the tenant name in the **HTTP header** to proceed.


2. **Body**:
   - After setting up the header, inform a valid body to insert a user. Example:  

	```json
	{  
	    "name": "Name of group",
        "attributes": {
        "additionalProp1": [
          "test1"
        ],
        "additionalProp2": [
          "test2"
        ],
        "additionalProp3": [
          "test3"
        ]
      }
	}
	
	```
---

### 📝 Reminder

The attributes property is not mandatory, but if you wanna configure some attribute to your group, you can defined it using this field.
An example about how attribute works, could be found below:

![Attributes definition](https://res.cloudinary.com/dd7cforjd/image/upload/cluh2nidozrkw1fgwnpl.jpg "Attributes definition")   

The JSON used to created this attribute was:

```json
"attributes": {
    "additionalProp1": [
      "test1"
    ],
    "additionalProp2": [
      "test2"
    ],
    "additionalProp3": [
      "test3"
    ]
  }
}
```
