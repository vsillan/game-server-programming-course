using Microsoft.AspNetCore.Builder;

namespace game_server.Middleware
{
    public static class MiddlewareExtensions
    {
        public static void UseGlobalErrorHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ErrorHandling.GlobalErrorHandler>();
        }
    }
}