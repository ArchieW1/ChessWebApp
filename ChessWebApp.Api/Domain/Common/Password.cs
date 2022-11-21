using ValueOf;
using FluentValidation;
using FluentValidation.Results;

namespace ChessWebApp.Api.Domain.Common;

public sealed class Password : ValueOf<string, Password>
{
    protected override void Validate()
    {
        if (Value.Length < 5)
        {
            string message = $"{Value} is not secure. Please include a minimum of 5 characters.";
            throw new ValidationException(message, new []
            {
                new ValidationFailure(nameof(EmailAddress), message)
            });
        }
    }
}