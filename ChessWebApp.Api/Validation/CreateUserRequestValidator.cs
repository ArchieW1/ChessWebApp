using ChessWebApp.Api.Contracts.Requests;
using FluentValidation;

namespace ChessWebApp.Api.Validation;

public sealed class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(request => request.Password).NotEmpty();
        RuleFor(request => request.Email).NotEmpty();
        RuleFor(request => request.Username).NotEmpty();
    }
}
