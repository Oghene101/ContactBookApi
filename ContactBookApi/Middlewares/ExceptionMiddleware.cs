using System.Net;
using System.Text.Json;
using ContactBookApi.Core.Dtos;
using ContactBookApi.Dtos;

namespace ContactBookApi.Middlewares;

public class ExceptionMiddleware(
    RequestDelegate next,
    ILogger<ExceptionMiddleware> logger,
    IHostEnvironment hostEnvironment)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (ArgumentNullException ex)
        {
            await HandleException(httpContext, ex, "Invalid argument", HttpStatusCode.BadRequest);
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleException(httpContext, ex, "Unauthorized access", HttpStatusCode.Unauthorized);
        }
        catch (Exception ex)
        {
            await HandleException(httpContext, ex, "An unexpected error occurred.", HttpStatusCode.InternalServerError);
        }
    }

    private async Task HandleException(HttpContext httpContext, Exception ex, string errorMessage,
        HttpStatusCode statusCode)
    {
        logger.LogError(ex, ex.Message);

        var response = httpContext.Response;
        response.ContentType = "application/json";

        var result =
            JsonSerializer.Serialize(ResponseDto.Failure(new Error[] { new("Server.Error", errorMessage) },
                statusCode));

        await response.WriteAsync(result);
    }
}