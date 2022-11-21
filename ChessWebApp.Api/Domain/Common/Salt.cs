using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace ChessWebApp.Api.Domain.Common;

public sealed class Salt : ValueOf<string, Salt>
{
    protected override void Validate()
    {
        if (DateTime.TryParse(Value, out _))
        {
            string message = $"{Value} is not a valid salt.";
            throw new ValidationException(message, new []
            {
                new ValidationFailure(nameof(Salt), message)
            });
        }
    }
}