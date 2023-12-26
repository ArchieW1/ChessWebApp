using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace ChessWebApp.Api.Domain.Common;

public sealed class EmailAddress : ValueOf<string, EmailAddress>
{
    private static readonly Regex EmailRegex =
        new("^[\\w!#$%&’*+/=?`{|}~^-]+(?:\\.[\\w!#$%&’*+/=?`{|}~^-]+)*@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.ExplicitCapture,
            TimeSpan.FromMilliseconds(50));

    protected override void Validate()
    {
        if (!EmailRegex.IsMatch(Value))
        {
            string message = $"{Value} is not a valid email address";
            throw new ValidationException(message, new []
            {
                new ValidationFailure(nameof(EmailAddress), message)
            });
        }
    }
}
