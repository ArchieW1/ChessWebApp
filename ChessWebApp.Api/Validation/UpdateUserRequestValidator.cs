using ChessWebApp.Api.Contracts.Requests;
using FluentValidation;

namespace ChessWebApp.Api.Validation;

public sealed class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(request => request.Email).NotEmpty();
        RuleFor(request => request.Username).NotEmpty();
        RuleFor(request => request.Password).NotEmpty();
    }
}
