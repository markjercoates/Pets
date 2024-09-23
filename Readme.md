# PetsAlone Technical Assesment

### Submission by Mark Coates

I've removed all the existing projects and started from scratch.
I've created a new .NET 8 WEB API Solution consisting of 6 projects:

- Pets.API
- Pets.Application
- Pets.Contracts
- Pets.Persistence
- Pets.UnitTests
- Pets.IntegrationTests

There is also an Angular v18 application in the client folder to display the UI.

EntityFrameworkCore is used to manage read write access to the SQLLite database Pets.db.
ASP.NET Core Identity is used to managed authentication and authorization
FluentValidation library is used to validate requests
The SQLLite db currently has about 10 records.

Unit tests validate the methods in the PetService class in the Application project mocking out the calls to the database repositories.
An Integration test is configured to run against an in memory database and does an end to end test to validate that missing pet
records are returned in the correct order with newest entry first.

The Angular app was built with the following:

- Node v20.17
- Angular v18.2.4
- Ng-Bootstrap

The client app is not bootstrapped by the API. Instead you'll need to do the following:

1. Ensure that the API is running. From within Visual Studio click the https profile or use dotnet run in a terminal.
2. run npm install in the root of the angular client folder and assuming all dependencies are resolved correctly
   then run ng serve to open a broswer at https:localhost:4200.
   (I've installed using makecert a self signed certificate to allow for https requests. If there are errors with this then revert to using http).
3. To sign in to create or edit a pet record use: admin@test.com Pa$$w0rd

There is an environment.development.ts file containing the API URL.

The API has Create, Update, Delete and Get endpoints and returns a paginated list of pet records.
The UI shows a paginated table of missing pets which can be filtered and searched and a login to create a new missing pet record and edit one.
