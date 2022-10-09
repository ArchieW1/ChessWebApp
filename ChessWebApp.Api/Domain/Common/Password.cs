using System.Text.RegularExpressions;
using ValueOf;
using FluentValidation;
using FluentValidation.Results;

namespace ChessWebApp.Api.Domain.Common;

public sealed class Password : ValueOf<string, Password>
{
    private static readonly Regex PasswordRegex =
        new("^(?=.*?[a-z])(?=.*?[0-9]).{5,}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    
    protected override void Validate()
    {
        if (!PasswordRegex.IsMatch(Value))
        {
            string message = $"{Value} is not secure. Please include 1 letter, 1 digit and a minimum of 5 characters.";
            throw new ValidationException(message, new []
            {
                new ValidationFailure(nameof(EmailAddress), message)
            });
        }
    }
}