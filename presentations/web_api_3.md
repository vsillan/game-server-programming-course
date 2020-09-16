# ASP.NET Core Web API part 3

---

## Filters

The filter pipeline runs after ASP.NET Web API selects the action to execute

Filters are a way of implementing cross-cutting concerns

- Avoiding duplicating code across actions

There are built in filters such as:

- Authorization
- Ensuring HTTPS usage

Possible filter scopes are global, controller and action

Notes:

Draw the entry to filter pipeline picture on the board

---

### Filters: Usage

```C#
[Authorize] //built-in filter - applies to all actions in the controller
public class SampleController : Controller {
	[OnlyTest] //custom filter - applies only to this action
    	public string Test(){
        		return "Hello world!";
	}
}

// In Startup.cs
public void ConfigureServices(IServiceCollection services) {
    services.AddMvc(options => {
        options.Filters.Add(new SampleGlobalActionFilter()); // custom filter - applies to all controllers and their actions
    });
}
```

---

### Filters: filter pipeline and filter types

![Filter pipeline](/resources/filter-pipeline.png)

---

### Filters: differences to middleware

Most important is the execution order:

- Filters run within the Web API filter pipeline
- Middleware runs before Web API kicks in

Filters can be specified per action – middleware only per URI

Note:

Filters can access the Web API context – Middleware can’t

---

### Exception filters 

The easiest solution for processing the subset of unhandled exceptions related to a specific action or a controller

As with other filters, exception filter can be registered by action, by controller or globally

```C#
public class NotImplExceptionFilterAttribute : ExceptionFilterAttribute {
	public override void OnException(ExceptionContext context){
		if (context.Exception is NotImplementedException){
			context.Result = new NotFoundResult();
		}
	}
}
```

---

## Model validation

Before a server stores data in a database, the server must validate the data (model)

- Check for potential security threats
- Appropriately formatted
- Conforms the rules of the application

Model validation is done automatically when Controller is decorated with [ApiContoller] attribute

- Returns HTTP Code 400 (Bad Request) when model is not valid

Note:

- Explain issues of under-posting and security threats for over-posting

---

### Model validation: built in attributes

- [EmailAdress]: Validates the property has an email format
- [Range]: Validates the property value falls within the given range
- [Required]: Makes a property required
- [StringLength]: Validates that a string property has at most the given maximum length

```C#
public class Item{
	public int Id { get; set; }
	[Required]
	public decimal? Price { get; set; }
	[Range(0, 99)]
	public double Weight { get; set; }
}
```

---

### Model Validation: Custom validation

Custom validation attributes are also possible:

```C#
public class UserOlderThan13Attribute : ValidationAttribute, IClientModelValidator {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
        Account account = (Account)validationContext.ObjectInstance;
        if (account.Age < 13) {
            return new ValidationResult(GetErrorMessage());
        }
        return ValidationResult.Success;
    }
```

---

## Logging

Logging is a way of getting information out of application to the developers and system administrators

Logging means writing information to an output such as:

- Console
- Text file
- Network socket

---

### Logging

Output format might need to be standard-based to enable easy parsing and processing

ASP.NET Core supports a logging API that works with a variety of logging providers

Built-in providers let you send logs to one or more destinations

There are many third-party logging providers which can be used with ASP.NET Core

You can also define you own providers

---

### Logging: Configuration

ASP.NET Core dependency injection (DI) provides the ILoggerFactory instance

The AddConsole and AddDebug extension methods call the ILoggerFactory.AddProvider method, passing in an instance of the provider

```C#
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    loggerFactory
        .AddConsole() //built in provider which outputs to console
        .AddDebug(); //built in provider which outputs to debug stream
```

---

### Logging: categories

Each of the log messages has a category, the category can be any string

- Convention is to use fully qualified name of the class: ”GameApi.Controllers.PlayersController”

Easiest way to set the category is to request the generic ILogger<T> in the class constructor which automatically sets the category

```C#
private ILogger<PlayersController> _logger;
public PlayersController(ILogger<PlayersController> logger) {
	_logger = logger;
}
```

---

### Logging: ASP.NET Core Log Levels

Each time you write a log, you specify its Log Level

The log level indicates the degree of severity or importance

ILogger has extension methods for writing logs with each log level:

\_logger.Information(...), \_logger.LogWarning(...), etc...

---

### Logging: ASP.NET Core Log Levels

#### Trace = 0

For information that is valuable only to a developer debugging an issue

These messages may contain sensitive application data and so should not be enabled in a production environment

---

### Logging: ASP.NET Core Log Levels

#### Debug = 1

For information that has short-term usefulness during development and debugging

You typically would not enable Debug level logs in production unless you are troubleshooting

Example: “Entering method Configure with flag set to true”

---

### Logging: ASP.NET Core Log Levels

#### Information = 2

For tracking the general flow of the application

These logs typically have some long-term value

Example: “Request received for path /players”

---

### Logging: ASP.NET Core Log Levels

#### Warning = 3

For abnormal or unexpected events in the application flow

These may include errors or other conditions that do not cause the application to stop, but which may need to be investigated

Handled exceptions are a common place to use the Warning log level

Example: ”FileNotFoundException for file game.txt”

---

### Logging: ASP.NET Core Log Levels

#### Error = 4

For errors and exceptions that cannot be handled

These messages indicate a failure in the current activity or operation (such as the current HTTP request)

Example: “Cannot insert record due to duplicate key violation”

---

### Logging: ASP.NET Core Log Levels

#### Critical = 5

For failures that require immediate attention

Example: data loss scenario

---

## Application environments

Environment is the place where the application lives
There should be many environments for different versions of the application

- Typically at least development, staging, and production

Environments can be used for things like

- Database connection information
- Error pages (specific in development – vague in production)
- Log-levels (verbose in development – only warnings and errors in production)

---

### Application environments

ASP.NET Core provides support for controlling app behavior across multiple environments

ASPNETCORE_ENVIRONMENT environment variable is used to decide the current environment for the application

- The value can be anything but Development, Staging and Production are used commonly
- You can set the variable on Windows cmd with “SET ASPNETCORE_ENVIRONMENT=Development”

---

### Application environments

The IHostingEnvironment service provides the core abstraction for working with environments

```C#
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
	if(env.IsDevelopment()) { ... }
```

The aim should be to have the environment checking –code in the startup class

---

### Application environments

ASP.NET Core supports a convention-based approach to configuring an application's startup based on the current environment

- if a class called Startup{EnvironmentName} (for example StartupDevelopment) exists and the environment name matches the value of ASPNETCORE_ENVIRONMENT, it is used instead of the normal Startup-class

Configure() and ConfigureServices() –methods work with the same principle

- You can use Configure{EnvironmentName}() and Configure{EnvironmentName}Services()
