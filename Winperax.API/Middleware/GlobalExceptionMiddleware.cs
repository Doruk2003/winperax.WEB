using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text.Json;
using Winperax.API.Responses;
using FluentValidation;

namespace Winperax.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = exception switch
            {
                ValidationException validationException => 
                    ApiResponse.FailureResult(
                        validationException.Errors.Select(e => e.ErrorMessage).ToArray(),
                        "Validation failed"
                    ),
                _ => 
                    ApiResponse.FailureResult(
                        new[] { "An unexpected error occurred." },
                        "Internal server error"
                    )
            };

            context.Response.StatusCode = exception is ValidationException ? 400 : 500;
            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}