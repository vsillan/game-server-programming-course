# Assignment 6

## 1. API-key authentication

The purpose of this exercise is to learn to implement a custom middleware for the Web API request processing pipeline. The middleware presented here will check for an **API-key** which is a simple way of preventing unwanted parties from accessing the API. You will also learn how to use JSON config files.

Create an authentication middleware which checks the request header for an API-key. The middleware should work as the following:

- Return HTTP code `400` (bad request) if `x-api-key` header is missing
- Return HTTP code `403` (forbidden) if the API-key does not match the one configured for the server
- If everything is okay, proceed forward in the request processing pipeline without responding to the client

The API-key should be defined in the configuration file `appsettings.json`.

**Implementation hints**

- Create a middleware (there is an example in the Web API slides) called `AuthMiddleware`
- Use `HttpContext` object to find the headers and for sending response to the client
- Register the `AuthMiddleware` in the `Startup.cs`
- Pass an api-key from a configuration file `appsettings.json` to the middleware through dependecy injenction

## 2. API-key authorization

Add an another API-key for admin clients. Limit the usage of deleting or banning players to only those clients who have the admin API-key.

- Read more on how to do it from here: https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims?view=aspnetcore-2.1

## 3. User action auditing

Create an action filter for auditing the usage of delete or ban endpoint in `PlayersController`. The audit should write to the `IRepository` two things:

- Who started deleted / banned and when (something like `An request from ip address {insertIpHere} to delete player started at 9:30:35 12.10 2018`)
- Who succesfully deleted / banned and when (something like `An request from ip address {insertIpHere} to delete player ended at 9:30:36 12.10 2018`)

Since the filter is using `IRepository`you need to implement this in both `MongoDbRepository` and `InMemoryRepository`.

**Hints**
- Read from here how to use dependency injection with filters: https://www.devtrends.co.uk/blog/dependency-injection-in-action-filters-in-asp.net-core
