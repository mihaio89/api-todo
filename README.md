# API versioning

## URL versioning
With this approach, the version number is included in the URL of the API endpoint. For instance, consumers who are interested in viewing all of the products in a database would send a request to the https://example-api.com/v1/products endpoint. This is the most popular type of API versioning.

## Query parameter versioning
This strategy requires users to include the version number as a query parameter in the API request. For instance, they might send a request to https://example-api.com/products?version=v1.

## Header versioning
This approach allows consumers to pass the version number as a header in the API request, which decouples the API version from the URL structure.

## Consumer-based versioning
This versioning strategy allows consumers to choose the appropriate version based on their needs. With this approach, the version that exists at the time of the consumer's first call is stored with the consumer's information. Every future call is then executed against this same version—unless the consumer explicitly modifies their configuration.

https://www.postman.com/api-platform/api-versioning/

# DI and EF

`builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));`

is part of the ASP.NET Core Dependency Injection (DI) setup and Entity Framework Core configuration.

Dependency Injection: ASP.NET Core uses dependency injection to provide services to various parts of the application, such as controllers, middleware, and other services. By registering the TodoDb context with the service collection, it becomes available for injection into other parts of the application.

Entity Framework Core: EF Core is an ORM (Object-Relational Mapper) that allows you to interact with a database using .NET objects. The DbContext class (in this case, TodoDb) is the primary class responsible for interacting with the database.

Registration: AddDbContext<TodoDb> registers the TodoDb context with the DI container.
Configuration: opt.UseInMemoryDatabase("TodoList") configures the TodoDb context to use an in-memory database named "TodoList".
Purpose: This setup enables the TodoDb context to be injected and used throughout the application, storing data in memory.

# Samples

https://github.com/dotnet/AspNetCore.Docs/tree/main/aspnetcore/tutorials/min-web-api/samples/8.x

# Reference
[Choose between controller-based APIs and minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/apis?view=aspnetcore-8.0)
[Minimal APIs overview](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/overview?view=aspnetcore-8.0)
[Organizing ASP.NET Core Minimal APIs](https://www.tessferrandez.com/blog/2023/10/31/organizing-minimal-apis.html)
[Route Handlers in Minimal API apps](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/route-handlers?view=aspnetcore-8.0)
[How to create responses in Minimal API apps](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/responses?view=aspnetcore-8.0#typedresults-vs-results)
[Testing a minimal API](https://github.com/dotnet/AspNetCore.Docs.Samples/tree/main/fundamentals/minimal-apis/samples/MinApiTestsSample)
[Naming tests](https://www.tessferrandez.com/blog/2023/11/08/naming-tests.html)
[Quickstart: Deploy an ASP.NET web app](https://learn.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=net70&pivots=development-environment-vs)

