using Domain.Exceptions;
using Domain.Exceptions.Base;
using Newtonsoft.Json;
using Serilog;

namespace api.Middleware;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            Log.Error(e, e.Message);
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = exception switch
        {
            BadRequestException or ValidationException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

        var errors = Array.Empty<ApiError>();

        if (exception is ValidationException validationException)
        {
            errors = validationException.Errors
                .Select(error => new ApiError(error.PropertyName, error.ErrorMessage))
                .ToArray();
        }

        var response = new
        {
            status = httpContext.Response.StatusCode,
            message = exception.Message,
            errors
        };

        var responseString = JsonConvert.SerializeObject(response);

        await httpContext.Response.WriteAsync(responseString);
    }

    private record ApiError(string PropertyName, string ErrorMessage);
}