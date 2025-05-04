using System.Diagnostics;
using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using TMS.Application.Common.Models;
using TMS.Core.Common.Errors;

namespace TMS.API.Middlewares;

/// <summary>
/// 
/// </summary>
public class ApiResponseMiddleware
{
    /// <summary>
    /// 
    /// </summary>
    private readonly RequestDelegate _next;
    
    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger<ApiResponseMiddleware> _logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    public ApiResponseMiddleware(RequestDelegate next, ILogger<ApiResponseMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        var originalBody = context.Response.Body;
        await using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        try
        {
            await _next(context);

            memoryStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
            memoryStream.Seek(0, SeekOrigin.Begin);

            context.Response.Body = originalBody;

            switch (context.Response.StatusCode)
            {
                case >= 200 and <= 201 when IsJsonResponse(context.Response):
                {
                    var jsonElement = JsonSerializer.Deserialize<JsonElement>(responseBody);

                    var apiResponse = new ApiResponse<object>
                    {
                        StatusCode = context.Response.StatusCode,
                        Title = "Success",
                        Timestamp = DateTimeOffset.Now,
                        TraceId = context.TraceIdentifier,
                        Path = context.Request.Path,
                        Method = context.Request.Method,
                        ExecutionTime = stopwatch.Elapsed.TotalMilliseconds,
                        Data = jsonElement
                    };

                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse, 
                        new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                    break;
                }
                
                case 404:
                {
                    var notFoundResponse = new ApiResponse<object?>
                    {
                        StatusCode = context.Response.StatusCode,
                        Title = "Failure",
                        Timestamp = DateTimeOffset.Now,
                        TraceId = context.TraceIdentifier,
                        Path = context.Request.Path,
                        Method = context.Request.Method,
                        ExecutionTime = stopwatch.Elapsed.TotalMilliseconds,
                        Data = null
                    };

                    context.Response.StatusCode = 404;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(notFoundResponse, 
                        new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                    break;
                }
                default:
                    await memoryStream.CopyToAsync(originalBody);
                    break;
            }
        }
        catch (Exception ex)
        {
            context.Response.Body = originalBody;

            if (!context.Response.HasStarted)
            {
                await HandleExceptionAsync(context, ex);
            }
            else
            {
                _logger.LogWarning("Cannot write error response: response has already started.");
                throw;
            }
        }
        finally
        {
            stopwatch.Stop();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    private static bool IsJsonResponse(HttpResponse response)
    {
        return response.ContentType?.Contains("application/json", StringComparison.OrdinalIgnoreCase) == true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="exception"></param>
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.Clear();
        context.Response.ContentType = "application/json";

        HttpStatusCode statusCode;
        string errorMessage;
        Dictionary<string, List<string>> validationErrors = new();

        switch (exception)
        {
            case ArgumentException or InvalidOperationException:
                statusCode = HttpStatusCode.BadRequest;
                errorMessage = exception.Message;
                _logger.LogError(exception,
                    "An error occurred with the following error message: {ErrorMessage}", exception.Message);
                break;

            case DbUpdateConcurrencyException concurrencyException:
                statusCode = HttpStatusCode.Conflict;
                errorMessage = $"{ErrorMessages.Database.ConcurrencyConflict}\n{concurrencyException.Message}";
                _logger.LogError(concurrencyException, "Concurrency conflict occured with the following error message: {ErrorMessage}", concurrencyException.Message);
                break;

            case DbUpdateException { InnerException: PostgresException postgresEx }:
                statusCode = HttpStatusCode.UnprocessableEntity;
                errorMessage = $"{ErrorMessages.Database.PostgresError}\n{postgresEx.Message}";
                _logger.LogError(postgresEx, "PostgreSQL error occurred with the following error message: {ErrorMessage} (Code: {ErrorCode})", postgresEx.Message, postgresEx.SqlState);
                break;

            case DbUpdateException dbUpdateEx:
                statusCode = HttpStatusCode.UnprocessableEntity;
                errorMessage = $"{ErrorMessages.Database.UpdateFailed}\n{dbUpdateEx.Message}";
                _logger.LogError(dbUpdateEx, "An error occurred while saving the entity to the database with the following error message: {ErrorMessage}", dbUpdateEx.Message);
                break;

            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                errorMessage = validationException.Message;
                validationErrors = validationException.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToList());
                _logger.LogError(validationException, "Validation error occured with the following error message: {ErrorMessage}", validationException.Message);
                break;
            
            default:
                statusCode = HttpStatusCode.InternalServerError;
                errorMessage = $"{ErrorMessages.Database.UnexpectedError}\n{exception.Message}";
                _logger.LogError(exception, "An unexpected error occurred with the following message: {ExceptionMessage}", exception.Message);
                break;
                
        }

        context.Response.StatusCode = (int)statusCode;

        var errorResponse = new ErrorResponse
        {
            TraceId = context.TraceIdentifier,
            Path = context.Request.Path,
            Method = context.Request.Method,
            StatusCode = (int)statusCode,
            Detail = errorMessage,
            Timestamp = DateTimeOffset.UtcNow,
            Errors = validationErrors,
        };

        _logger.LogError(exception, "Handled exception: {Message}", exception.Message);

        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, 
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
    }
}
