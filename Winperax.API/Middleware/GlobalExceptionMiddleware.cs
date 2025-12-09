using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Winperax.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger
        )
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

            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred.",
                Instance = context.Request.Path,
            };

            // Validation hataları için özel işlem
            if (exception.GetType().Name == "ValidationException")
            {
                problemDetails.Status = (int)HttpStatusCode.BadRequest;
                problemDetails.Title = "Validation Failed";
                problemDetails.Detail = string.Join(
                    "; ",
                    ((FluentValidation.ValidationException)exception).Errors.Select(e =>
                        e.ErrorMessage
                    )
                );
            }

            var jsonResponse = System.Text.Json.JsonSerializer.Serialize(problemDetails);
            context.Response.StatusCode = problemDetails.Status!.Value;
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
