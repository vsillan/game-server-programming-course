# Assignment 6

The purpose of this exercise is to learn to implement a custom middleware for the Web API request processing pipeline. The middleware presented here will check for an __API-key__ which is a simple way of preventing unwanted parties from accessing the API. You will also learn how to use JSON config files.

Create an authentication middleware which checks the request header for an API-key. The middleware should work as the following:

- Return HTTP code ``400`` (bad request) if ``x-api-key`` header is missing
- Return HTTP code ``403`` (forbidden) if the API-key does not match the one configured for the server
- If everything is okay, proceed forward in the request processing pipeline without responding to the client

The API-key should be defined in the configuration file ``appsettings.json``.

## Reading list

- Configuration: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.1&tabs=basicconfiguration
- Middlewares: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-2.1&tabs=aspnetcore2x

**Implementation hints**

- Create a middleware (there is an example in the Web API slides) called ``AuthMiddleware``
- Use ``HttpContext`` object to find the headers and for sending response to the client
- Register the ``AuthMiddleware`` in the ``Startup.cs``
- Pass an api-key from a configuration file ``appsettings.json`` to the middleware through dependecy injenction