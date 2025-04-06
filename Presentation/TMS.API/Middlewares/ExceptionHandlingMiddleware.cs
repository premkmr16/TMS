using System.Net;
using System.Runtime.CompilerServices;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using TMS.Core.Common.Errors;

namespace TMS.API.Middlewares;

/// <summary>
/// Middleware for handling global exceptions in the application.
/// Logs exceptions and returns a standardized error response.
/// </summary>
public class ExceptionHandlingMiddleware
{
    /// <summary>
    /// Delegate that represents the next middleware in the pipeline.
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// Logger instance for recording exception details.
    /// </summary>
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware delegate in the pipeline.</param>
    /// <param name="logger">Logger for logging errors.</param>
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Middleware execution method that handles exceptions.
    /// </summary>
    /// <param name="context">HTTP context for the current request.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    /// <summary>
    /// Handles exceptions, logs them, and returns a standardized error response.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <param name="exception">The exception that occurred.</param>
    /// <param name="methodName">The name of the method where the exception occurred.</param>
    /// <param name="filePath">The file path where the exception originated.</param>
    private async Task HandleExceptionAsync(HttpContext context, Exception exception,
        [CallerMemberName] string methodName = "", [CallerFilePath] string filePath = "")
    {
        context.Response.ContentType = "application/json";

        string errorMessage;
        HttpStatusCode statusCode;
        Dictionary<string, List<string>> validationErrors = new();

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                errorMessage = validationException.Message;

                validationErrors = validationException.Errors.GroupBy(x => x.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToList());

                _logger.LogError(validationException,
                    "File: {File} | Method: {Method} | Validation error occured with the following error message: {ErrorMessage}",
                    filePath, methodName, validationException.Message);
                break;

            case DbUpdateConcurrencyException concurrencyException:
                statusCode = HttpStatusCode.Conflict;
                errorMessage = $"{ErrorMessages.Database.ConcurrencyConflict}\n{concurrencyException.Message}";

                _logger.LogError(concurrencyException,
                    "File: {File} | Method: {Method} | Concurrency conflict occured with the following error message: {ErrorMessage}",
                    filePath, methodName, concurrencyException.Message);
                break;

            case DbUpdateException { InnerException: PostgresException postgresException }:
                statusCode = HttpStatusCode.UnprocessableEntity;
                errorMessage = $"{ErrorMessages.Database.PostgresError}\n{postgresException.Message}";

                _logger.LogError(postgresException,
                    "File: {File} | Method: {Method} | PostgreSQL error occurred with the following error message: {ErrorMessage} (Code: {ErrorCode})",
                    filePath, methodName, postgresException.Message, postgresException.SqlState);
                break;

            case DbUpdateException dbUpdateException:
                statusCode = HttpStatusCode.UnprocessableEntity;
                errorMessage = $"{ErrorMessages.Database.UpdateFailed}\n{dbUpdateException.Message}";

                _logger.LogError(dbUpdateException,
                    "File: {File} | Method: {Method} | An error occurred while saving the entity to the database with the following error message: {ErrorMessage}",
                    filePath, methodName, dbUpdateException.Message);
                break;

            default:
                statusCode = HttpStatusCode.InternalServerError;
                errorMessage = $"{ErrorMessages.Database.UnexpectedError}\n{exception.Message}";

                _logger.LogError(exception,
                    "File: {File} | Method: {Method} | An unexpected error occurred with the following message: {ExceptionMessage}",
                    filePath, methodName, exception.Message);
                break;
        }

        var response = new
        {
            StatusCode = statusCode,
            ErrorMessage = errorMessage,
            Timestamp = DateTimeOffset.UtcNow,
            Errors = validationErrors
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}