using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace game_server.ErrorHandling
{
    public class GlobalErrorHandler
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NotImplementedException e)
            {
                httpContext.Response.StatusCode = 404;
                await httpContext.Response.WriteAsync("Not found");
            }
        }
    }
}