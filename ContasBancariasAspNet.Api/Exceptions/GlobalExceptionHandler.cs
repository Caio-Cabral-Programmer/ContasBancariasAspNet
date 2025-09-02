using ContasBancariasAspNet.Api.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace ContasBancariasAspNet.Api.Extensions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var response = httpContext.Response;
        response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            NotFoundException ex => (HttpStatusCode.NotFound, ex.Message),
            BusinessException ex => (HttpStatusCode.UnprocessableEntity, ex.Message),
            _ => (HttpStatusCode.InternalServerError, "Unexpected server error.")
        };

        response.StatusCode = (int)statusCode;

        if (statusCode == HttpStatusCode.InternalServerError)
        {
            _logger.LogError(exception, "Unexpected error occurred");
        }

        await response.WriteAsync(message, cancellationToken);
        return true;
    }
}