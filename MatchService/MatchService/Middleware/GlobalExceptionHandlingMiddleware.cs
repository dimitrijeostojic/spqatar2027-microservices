using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MatchService.Middleware;

public sealed class GlobalExceptionMiddleware(
    ILogger<GlobalExceptionMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails
            {
                Status = 500,
                Title = "Server error",
                Detail = "An internal server error has occurred."
            }));
        }
    }
}
