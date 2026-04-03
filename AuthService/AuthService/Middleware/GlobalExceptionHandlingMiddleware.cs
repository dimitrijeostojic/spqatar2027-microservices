
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AuthService.Middleware;

public sealed class GlobalExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            ProblemDetails problemDetails = new()
            {
                Status = context.Response.StatusCode,
                Type = "Server error",
                Title = "Server error",
                Detail = "An internal server has occured"
            };

            var json = JsonSerializer.Serialize(problemDetails);


            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}
