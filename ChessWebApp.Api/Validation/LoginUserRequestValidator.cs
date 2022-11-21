using ChessWebApp.Api.Contracts.Requests;
using FluentValidation;

namespace ChessWebApp.Api.Validation;

public sealed class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserRequestValidator()
    {
        RuleFor(request => request.Username).NotEmpty();
        RuleFor(request => request.Password).NotEmpty();
    }
}