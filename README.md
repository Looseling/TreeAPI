# TreeAPI
 
 an ASP.NET Core 6 application that provides a RESTful API for managing in-dependent trees and logging exceptions. 
 The application uses code-first migration and supports MsSQL

## Database Model

The application is designed to store in-dependent trees consisting of nodes. Each node belongs to a single tree, and child nodes belong to the same tree as their parent node. The node model includes a mandatory field, "name," and allows optional additional fields. The database model is flexible and can be extended as needed for managing in-dependent trees.

## Exception Handling

The TreeAPI handles exceptions during request processing and maintains a journal of all exceptions.


## REST API

The TreeAPI provides a RESTful API for managing the in-dependent trees. The API offers the following endpoints:


- **POST api/usertree**: Creates a new tree.

- **POST /api/UserTreeNode**: Creates new node in the tree.
- **POST /api/UserTreeNode/deleteNode**: deletes new node in the tree.
- **POST /api/UserTreeNode/renameNode**: renames new node in the tree.

- **POST /api/UserJournal/GetRange**: Retrieves Journals between 2 dates, has properties to skip and take from output of result
- **POST /api/UserJournal/GetSingle**: Retrieves a specific Journal by its ID.

Please refer to the Swagger documentation for detailed information on each endpoint and their expected request/response formats.



## Installation

To run the TreeAPI project, follow these steps:

1. Clone the repository by running the following command in your terminal or command prompt:

2. Open the project in your preferred IDE.

3. Open the `appsettings.json` file and locate the `ConnectionStrings` section. Add the appropriate connection string for MsSQL in the corresponding `DefaultConnection`.

4. Open the NuGet Package Manager Console in Visual Studio by going to `Tools` > `NuGet Package Manager` > `Package Manager Console`.

5. In the Package Manager Console, run the following command to generate the initial migration:

6. After the migration is created, run the following command to apply the migration and update the database:


7. At this point, the database is set up with the necessary tables and schema.

Now, the TreeAPI application should be up and running, and you can start making requests to the API endpoints.




Clone the repository,
