namespace ChessWebApp.Shared.Contracts;

public sealed class UserDto
{
    public string Id { get; init; } = default!;

    public string Username { get; init; } = default!;
    
    public string Email { get; init; } = default!;

    public string Password { get; init; } = default!;

    public int Wins { get; init; }

    public int Losses { get; set; }
}