using ChessWebApp.Api.Domain.Common;

namespace ChessWebApp.Api.Domain;

public sealed class User
{
    public Username Username { get; init; } = default!;
    
    public EmailAddress Email { get; init; } = default!;

    public Password Password { get; init; } = default!;

    public Salt Salt { get; set; } = default!;

    public Wins Wins { get; init; } = default!;

    public Losses Losses { get; init; } = default!;
}
