using System.Net;
using System.Text.Json;
using Application.CustomException;

namespace WebAPI
{
    public class ProblemDetailsMiddleware
    {
        private readonly RequestDelegate _next;
        public ProblemDetailsMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught in middleware: {ex.Message}");

                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/problem+json";

            var (statusCode, detail, errors) = GetExceptionDetails(exception);

            context.Response.StatusCode = statusCode;

            var problemDetails = new
            {
                type = $"https://httpstatuses.com/{statusCode}",
                title = GetTitleForStatusCode(statusCode),
                status = statusCode,
                detail,
                instance = context.Request.Path,
                errors = errors
            };

            var json = JsonSerializer.Serialize(problemDetails);

            return context.Response.WriteAsync(json);
        }

        private (int statusCode, string detail, string[] errors) GetExceptionDetails(Exception exception)
        {
            Exception exceptionCuston = exception.InnerException ?? exception;
            return exceptionCuston switch
            {
                ValidationCustomException validationException => (
                    (int)HttpStatusCode.BadRequest,
                    "One or more errors returned on validation",
                    validationException.Message.Split(',')
                ),
                BadRequestCustomException badRequestException => (
                    (int)HttpStatusCode.BadRequest,
                    badRequestException.Message,
                    new[] { badRequestException.Message }
                ),
                ArgumentNullException argumentNullException => (
                    (int)HttpStatusCode.NotFound,
                    argumentNullException.Message,
                    new[] { argumentNullException.Message }
                ),
                UnauthorizedAccessException unauthorizedAccessException => (
                    (int)HttpStatusCode.Unauthorized,
                    unauthorizedAccessException.Message,
                    new[] { unauthorizedAccessException.Message }
                ),
                DuplicateRecordException duplicateRecordException => (
                    (int)HttpStatusCode.Conflict,
                    duplicateRecordException.Message,
                    new[] { duplicateRecordException.Message }
                ),
                _ => (
                    (int)HttpStatusCode.InternalServerError,
                    exception.Message,
                    new[] { exception.Message }
                )
            };
        }

        private string GetTitleForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Invalid request",
                401 => "Unauthorized",
                409 => "Conflict",
                500 => "Internal server error",
                _ => "Unexpected error"
            };
        }
    }
}
