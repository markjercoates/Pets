# PetsAlone Technical Assesment

Picture the scene…you’ve tuned into your favourite show on Netflix ready to relax for the evening…and the phone rings…

It transpires that your friend, Bill, has lost his beloved pet – a rare breed Norwegian Forest Cat. In fact it’s been about 48 hours and Bill is rather distressed.

Thankfully Bill hasn’t asked you to go out searching on foot. However, knowing that you’re a keen technologist, he asks if you could help him out in a different way. He has had an idea of creating a web-based system to help owners raise awareness of their missing pets.

Bill has come up with a name (which he’s very proud of) ‘PetsAlone’, and he has also given you a head-start and made it available on GitHub, which you can clone and then modify to meet his requirements… You begrudgingly agree to help, knowing that Bill isn’t an accomplished developer, but he’s in a desperate situation and you want to help out. You manage to get some high-level requirements from Bill before he hangs up...

### User stories

As an anonymous user  
I want to see all missing pets  
So that I can help to reunite a missing pet with its owner.

As an authenticated user  
I want to create a new missing pet on the system  
So that I can raise awareness of a missing pet.

### Assessment Acceptance criteria

1.  The home page of the site will show all the missing pets, with the most recently added pets listed first
2.  A user will be able to filter the pets that have gone missing by animal type (dog, cat, ferret etc)
3.  A new page, only accessible by an authenticated user, which will be able to create a new missing pet. Bill has created a model to use in PetsAlone.Core/Pet.cs
4.  A developer picking up the project after you should see some evidence of you testing the code, even if it only covers a few key scenarios

### To help save you some time...

- The ‘PetsAlone.Core’ project found in this GitHub repo contains a service with some existing methods to get you started. It interacts with an in-memory database which can be used in this exercise in place of a ‘real’ database.

- This project also contains a mocked identity provider which can be used to simulate logging in. For an authenticated user you can log in with the following credentials:  
  Username: elmyraduff  
  Password: MontanaMax!!

- You’ll find ‘boilerplate’ projects for React, MVC Razor and Angular in the repo as well (look inside the 'web' folder'). Feel free to use one of these to get you started on the frontend, but if you’d prefer to start from scratch (or use a different technology or framework) that’s also fine. Remember - Bill is not an experienced developer, so don't be shy about changing or removing anything that you don't need.

- Bill needs this app live ASAP, so aim to spend only a couple of hours on this.

### Running the app

Ensure you have .net core 3.1 installed on your machine. If you're using Visual Studio 2019 then set one of the web projects as the startup project and hit run.

### Submission by Mark Coates

I've removed all the existing projects and started from scratch.
I've created a new .NET 8 WEB API Solution consisting of 6 projects:

- PetsAlone.API
- PetsAlone.Application
- PetsAlone.Contracts
- PetsAlone.Persistence
- PetsAlone.UnitTests
- PetsAlone.IntegrationTests

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
