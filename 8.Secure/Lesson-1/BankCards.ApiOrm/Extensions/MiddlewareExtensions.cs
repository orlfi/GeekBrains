using BankCards.ApiOrm.Middleware;

namespace BankCards.ApiOrm.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app, bool isDevelopment)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>(isDevelopment);
        return app;
    }
}
