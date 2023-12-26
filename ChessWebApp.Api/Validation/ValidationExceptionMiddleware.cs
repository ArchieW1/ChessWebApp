using ChessWebApp.Api.Contracts.Responses;
using FluentValidation;

namespace ChessWebApp.Api.Validation;

public sealed class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _request;

    public ValidationExceptionMiddleware(RequestDelegate request)
    {
        _request = request;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _request(context);
        }
        catch (ValidationException exception)
        {
            context.Response.StatusCode = 400;
            List<string> messages = exception.Errors.Select(failure => failure.ErrorMessage).ToList();
            ValidationFailureResponse validationFailureResponse = new()
            {
                Errors = messages
            };
            await context.Response.WriteAsJsonAsync(validationFailureResponse);
        }
    }
}
