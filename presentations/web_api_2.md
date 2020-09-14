# ASP.NET Core Web API part 2

---

## Simplified processing pipeline

![ASP.NET Pipeline](/resources/aspnet-simple-pipeline.png)

---

## Middleware

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

```C#
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

Middlewares are run in the registration order

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

## Error handling

Requests that are not handled by your app will be handled by the server

General error handling can be done in a middleware

In ASP.NET Core MVC pipeline the exceptions can be handled with filters

Note:

There are two major cases for handling exceptions, the case where we are able to send an error response and the case where all we can do is log the exception

---

### Error handling middleware

Should be the first middleware to guarantee that exceptions from other middlewares are caught

There are many built-in middleware for handling errors such as

- UseDeveloperExceptionPage() – Prints detailed error page with the call stack and much more
- UseStatusCodePages() – Prints a friendly page defining the http status code
- UseExceptionHandler() – Forwards the errored requests to another route

You can also create your own error handling middleware

---

### Error handling middleware

```C#
public class CustomExceptionHandlerMiddleware {
    private readonly RequestDelegate _next;
	public CustomExceptionHandlerMiddleware(RequestDelegate next) {
		_next = next;
	}
	public async Task Invoke(HttpContext context){
	  try {
		await _next(context)
	  };
	  catch(Exception e){
		//...
	  }
	}
}
```
