# CHALLENGE 4: API ENDPOINT
This is a simple Functions App with a few end points to create, read, list, update and delete food, from Table Storage, that Ezra's friends will bring to a holiday potluck.

# End Points
## Create
```
POST http://localhost:7071/api/food

{
	"name": "Swedish Meatballs",
	"description": "The Best Swedish Meatballs are smothered in the most amazing rich and creamy gravy",
	"fromWho": "Kevin"
}
```
returns ```201 Created``` with a URI to the newly created item or ```400 Bad Request``` with a error message if the json isn't valid
## Read
```
GET http://localhost:7071/api/food/{id}
```
returns ```200 OK``` if successful or ```404 Not Found``` if the entity can't be found

## Update
```
PUT http://localhost:7071/api/food/{id}

{
	"name": "Danish Meatballs",
	"description": "The Best Danish Meatballs are smothered in the most amazing rich and creamy gravy",
	"fromWho": "Not Kevin"
}
```
returns ```200 OK``` if successful or ```404 Not Found``` if the entity can't be found
## Delete
```
DELETE http://localhost:7071/api/food/{id}
```
returns ```204 No Content``` if successful or ```404 Not Found``` if the entity can't be found

## List
```
GET http://localhost:7071/api/food/
```
returns ```200 OK``` if successful. 
This action will only return the first 1000 items.

