
using System.Text;
using Microsoft.AspNetCore.Builder;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Linq;

namespace Timesheets.Infrastructure.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseExceptionsHandling(this IApplicationBuilder app)
        {
            app.Use(async (ctx, next) =>
            {
                try
                {
                    await next();
                }
                catch (ValidationException e)
                {
                    var response = ctx.Response;
                    if (response.HasStarted)
                        throw;

                    response.Clear();
                    response.StatusCode = 422;

                    await response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        Message = "Invalid data has been submitted",
                        ModelState = e.Errors.ToDictionary(error => error.ErrorCode, error => error.ErrorMessage)
                    }), Encoding.UTF8, ctx.RequestAborted);
                }
            });
        }
    }
}