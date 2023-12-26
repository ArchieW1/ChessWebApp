using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace ChessWebApp.Api.Domain.Common;

public sealed class Username : ValueOf<string, Username>
{
    private static readonly Regex UsernameRegex =
        new("^([a-zA-Z0-9]+){0,30}$", RegexOptions.IgnoreCase | RegexOptions.Compiled |
                                      RegexOptions.ExplicitCapture, TimeSpan.FromMilliseconds(50));

    protected override void Validate()
    {
        if (!UsernameRegex.IsMatch(Value))
        {
            string message = $"{Value} is not a valid username";
            throw new ValidationException(message, new []
            {
                new ValidationFailure(nameof(Username), message)
            });
        }
    }
}
