using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BankCards.Exceptions;

namespace BankCards.ApiOrm.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly bool _isDevelopment;

        public ErrorHandlingMiddleware(RequestDelegate next, bool isDevelopment, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _isDevelopment = isDevelopment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            object errors = null;

            switch (ex)
            {
                case DbException db:
                    _logger.LogError(ex, "Database error {0} {1}", db.Code, db.Errors);
                    errors = db.Errors;
                    context.Response.StatusCode = (int)db.Code;
                    break;
                case ApiException api:
                    _logger.LogError(ex, "Api error {0}", api.Code);
                    errors = api.Errors;
                    context.Response.StatusCode = (int)api.Code;
                    break;
                default:
                    _logger.LogError(ex, "Server error");
                    errors = _isDevelopment ? (string.IsNullOrWhiteSpace(ex.Message) ? "error" : ex.Message) : "Server Error";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "appliation/json";

            var result = JsonSerializer.Serialize(new { errors });

            await context.Response.WriteAsync(result);
        }
    }
}