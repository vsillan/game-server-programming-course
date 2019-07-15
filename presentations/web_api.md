# ASP.NET Core Web API

---

## Web API: what can we do with it?

Slower paced (asynchronous) games

Validating single player game sessions

Supporting systems

- account management, leaderboards, cloud-saves etc.

Note:

Not-so-great for creating real-time multiplayer games!

---

## ASP.NET Core

ASP.NET Core is a framework for building web UI and web APIs
Why ASP.NET Core?

- Cross-platform
- High-performance
- Open-source framework
- Many necessary features built-in such as logging, environment-based configuration, dependency injection etc.
- Cloud-ready

Note:

- Was developed as an alternative for Windows Communication Foundation (WCF)
- Not just large enterprises talking with each other anymore, now a developer needed to be able to whip up a JavaScript application, or 99-cent mobile app, in a matter of days
- Controller method need only return the raw data value, and this value will be automatically converted to JSON or XML, (format is decided by the client)

---

## Before we get into the business…

---

### HTTP messaging

HTTP is a communication protocol, that is used to deliver data on the World Wide Web

HTTP is based on the client-server architecture model

A stateless request/response protocol

Operates by exchanging messages across a reliable TCP/IP connection

---

### HTTP messaging

![HTTP messaging](/resources/http-messaging.png)

---

### HTTP request

HTTP request contains:

- Protocol
- Path
- Query string
- Headers
- Body

---

### HTTP GET request example

GET /hello.htm HTTP/1.1

Host: www.somerandomadress.com

Content-Type: application/json

Accept-Language: en-us

Accept-Encoding: gzip, deflate

Connection: Keep-Alive

---

### HTTP POST request example

POST /randomThings/ HTTP/1.1

Host: www.somerandomadress.com

Content-Type: application/json

Content-Length: length

Accept-Language: en-us

Accept-Encoding: gzip, deflate

Connection: Keep-Alive

{ licenseID: 123, content: "stuff"}

---


### HTTP response

HTTP response contains:

- Protocol
- Path
- Headers
- Body
- Status Code

---

### HTTP response example

HTTP/1.1 400 Bad Request

Date: Sun, 18 Oct 2017 10:36:20 GMT

Server: Apache/2.2.14 (Win32)

Content-Length: 230

Content-Type: application/json

Connection: Closed

{ message: "You did a bad request" }

---

## Now back to ASP.NET Core...

---

## ASP.NET Core processing pipeline

---

### Simplified processing pipeline

![ASP.NET Pipeline](/resources/aspnet-simple-pipeline.png)

---

### Middleware

The first stage in the processing pipeline

Software that is assembled into an application pipeline to handle requests and responses

Each middleware chooses whether to pass the request on or stop the processing

Can perform certain actions before and after the next middleware is invoked in the pipeline

---

### Middleware

![ASP.NET Middleware](/resources/aspnet-middleware.png)

---

### Middleware: Example

Here is the simplest possible middleware:

``` C#
public class Startup {
    public void Configure(IApplicationBuilder app) {
        app.Run(async context => {
            await context.Response.WriteAsync("Hello, World!");
        });
    }
}
```

App.Run(…) ends the pipeline

---

### Middleware: configuration

You can chain multiple request delegates together with app.Use

```C#
public void Configure(IApplicationBuilder app) {
    app.Use(async (context, next) =>{
        // Do work that doesn't write to the Response.
        await next.Invoke();
        // Do other work that doesn't write to the Response.
    });
    app.Run(async context => {
        await context.Response.WriteAsync("Hello, World!");
    });
}
```

The next parameter represents the next delegate in the pipeline

Order of the registration determines the execution order

Note:

You can end the pipeline by not calling next

---

### Middleware: configuration

![ASP.NET Middleware Configuration](/resources/aspnet-middleware-conf.png)

---

### Middleware: custom middleware

```C#
public class TimeCounterMiddleware{
    private readonly RequestDelegate _next;

    public TimeCounterMiddleware(RequestDelegate next){
        _next = next;
    }

    public async Task Invoke(HttpContext context){
        var watch = new Stopwatch();
        watch.Start();
        await _next(context);
        context.Response.Headers.Add("X-Processing-Time-Milliseconds", 
		new[] { watch.ElapsedMilliseconds.ToString() });
    }
}
```

---

### Middleware: custom middleware

```C#
//in MiddlewareExtensions.cs
public static class MiddlewareExtensions {
	public static IApplicationBuilder UseProcessingTimeCounterMiddleware(this IApplicationBuilder builder) {
        		return builder.UseMiddleware<ProcessingTimeMiddleware>();
    }
}

// in Startup.cs
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
    ...
    app.UseProcessingTimeCounterMiddleware(); // Could be also without the extension method: 												       // app.UseMiddleware<AuthorizationMiddleware>();
    ...
}
```

---

## Controllers

A controller is used to define and group a set of actions

An action (or action method) is a method on a controller which handles requests

Controller is an instantiable class in which at least one of the following conditions is true:

- Name is suffixed with "Controller“
- Inherits from a class whose name is suffixed with "Controller"
- Decorated with the [Controller] attribute

---

### Controller responbilities

In well-factored apps, controller does not directly include data access or business logic

- Controller delegates to services handling these responsibilities

Responsibilities:

- Ensure request data is valid
- Choose which result for an API should be returned

Note:

Explain model binding

- there is no contract between client and server
- convention over configuration
- Foundational principle in REST

Allows service to benefit from model binding, controller-specific and action-specific filters, and result conversion

Actions can return anything

- frequently return an instance of IActionResult (or Task<IActionResult> for async methods) that produces a response

---

## Routing to Controller Actions

Routing middleware is used to match the URLs of incoming requests and map them to actions

Routes are defined in startup code (convention based) or with attributes

Both ways of routing can be mixed together

If no routing attribute is defined, convention based routing is used

---

## Routing to Controller Actions

Web API routes are often defined with attribute route – convention based routes are often used for controllers serving HTML pages for browsers

- Which is why we skip the convention based routes now

Attribute routing is enabled when app.UseMvc(…) is called on the Startup.cs -file

---

### Attribute routing

Example:

```C#
[Route("players/{id}/items")]
public Item[] GetItemsByPlayer(int id) { ... }
```

Route attribute are used to map actions directly to route templates

Parameters are defined inside “{}” in the route

- MVC tries to bind URI parameter -> method parameter

Route can also use Http[verb]Attributes:

```C#
[HttpPost("/players")]
public Player AddPlayer() { ... }
```

---

### Attribute routing: combining routes

Route attributes on the controller are combined with route attributes on the individual actions

- Makes routing less repetitive

```C#
[Route("api/players")]
public class PlayersController : Controller {

   [HttpGet] // Matches '/api/players'
   public IActionResult List() { ... }

   [HttpPost("{id}")] // Matches '/api/players/{id}'
   public IActionResult Edit(int id) { ... }
}
```

---

### Route constraints

Define how the parameters in the route templates are matched

The general syntax is “{parameter:constraint}”:

```C#
[Route("players/{id:int}"]
public Player GetPlayerById(int id) { ... }
[Route("players/{name}"]
public Player GetPlayerByName(string name) { ... }
```

- If the id part of the URI is an integer, the first route will be used, otherwise the second one

Multiple constraints can be combined:

```C#
[Route("players/{id:int:min(1)}"]
public Player GetPlayerById(int id) { ... }
```

---

## Model binding

Setting the values for the parameters when a action (method) on a controller is called

URI parameters are matched by name to the action method parameters

- Works with simple types (int, string, bool…), collections and classes

Binding behavior can be customized using attributes: [FromHeader], [FromQuery], [FromRoute], [FromForm] and[FromBody]

Note:

Simple types include the .NET primitive types (int, bool, double, and so forth), plus TimeSpan, DateTime, Guid, decimal, and string, plus any type with a type converter that can convert from a string.

Complex types are read using a media-type (MIME type) formatter

A media type, also called a MIME type, identifies the format of a piece of data. In HTTP, media types describe the format of the message body. A media type consists of two strings, a type and a subtype. For example: text/html

How to force web api to change its behavior:

- Read complex type from the URI by adding [FromUri] attribute to the parameter
- Read simple type from the body by adding [FromBody] attribute to the parameter
- You can make Web API treat a class as a simple type by creating a TypeConverter and providing a string conversion.
- You can make custom model binders and have access to things like the HTTP request, the action description, and the raw values from the route data

---

### Model binding: binding object

Requirements for the class defining the model:

- Public constructor
- Public setter methods for the properties

It’s advisable to use request body to pass objects when possible

- Http verb GET does not allow request body

---

### Model binding: data formats

Request data can come in a variety of formats

- including JSON, XML and many others

MVC uses a configured set of formatters to handle the request data based on its content type

- Supports only JSON by default

---

## Dependency injection

---

### Dependency injection

Software design pattern that implements inversion of control for resolving dependencies

Dependency Inversion Principle: _“high level modules should not depend on low level modules; both should depend on abstractions”_

Instead of referencing specific implementations, classes request abstractions (typically interfaces) which are provided to them when the class is constructed

---

### Dependency injection: Implementation

Classes request their dependencies via their constructor (or properties and methods)

```C#
public class PlayersController : Controller {
	public PlayersController(IPlayerRepository repository) { ...}
}
```

There is usually a class which creates all of the required dependencies for the application

- This class is called Inversion of Control (IoC) container or Dependency Injection (DI) container
 
ASP.NET Core has a built-in DI-container


Note:

If the controllers are going to do anything useful, and if they are going to be implemented in a well-architected manner using SOLID design principles, they will depend heavily upon functionality provided by other classes. 

Includes even seemingly harmless classes such as System.DateTime, System.IO.File, System.Environment

A single container instance must meet these criteria: 

- Be created early in the application start-up process
- Be available at all times while the application is running
- Be destroyed as one of the lst steps the application takes during shutdown

ASP.NET Core container supports only constructor injection by default
  
---

### Dependency injection: Implementation(2)

The ConfigureServices method in the Startup class is responsible for defining the services the application will use

```C#
public void ConfigureServices(IServiceCollection services){
    services.AddDbContext<ApplicationDbContext>(
	options => options.UseInMemoryDatabase()); // Add database 	context using extension method
    services.AddMvc(); // Add framework services using extension method
    services.AddScoped<IPlayerRepository, PlayerRepository>(); // Register class PlayerRepository as IPlayerRepository
}
```

IServiceCollection has methods AddTransient, AddScoped and AddSingleton for defining the lifetime for a service

Note:
- Explain that dependencies can be complex graphs

---

### Dependency injection: service lifetimes - Transient

- Transient lifetime services are created each time they are requested
- This lifetime works best for lightweight, stateless services

---

### Dependency injection: service lifetimes - Scoped

- Scoped lifetime services are created once per request

---

### Dependency injection: service lifetimes - Singleton

- Singleton lifetime services are created the first time they are requested
- Every subsequent request will use the same instance

---

### The processing pipeline beyond the controller in _our_ Web API

Controller -> Repository -> Database

Controller has the following responsibilities:

- Defining web API routes
- Handling business logic

Repository handles the data access logic

Note:

This is up to the application developer to decide

Repository pattern: Essentially, it provides an abstraction of data, so that your application can work with a simple abstraction that has an interface approximating that of a collection.

Business logic means applying business rules to the data (how it can be created, displayed, stored and changed)
