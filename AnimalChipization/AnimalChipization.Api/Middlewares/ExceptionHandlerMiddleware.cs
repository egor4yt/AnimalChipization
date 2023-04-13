using System.Net;
using AnimalChipization.Core.Exceptions;

namespace AnimalChipization.Api.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (ApiException exception)
        {
            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)exception.HttpStatusCode;
            _logger.LogWarning(exception.ApiMessage);
            await context.Response.WriteAsync(exception.ApiMessage);
        }
        catch (AggregateException exception)
        {
            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync("something went wrong:");

            foreach (var innerException in exception.InnerExceptions)
            {
                await context.Response.WriteAsync($" {exception.Message};");
                _logger.LogError(innerException, "Part of aggregate exception");
            }
        }
        catch (Exception exception)
        {
            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync($"something went wrong: {exception.Message}");
            _logger.LogError(exception, exception.Message);
        }
    }
}