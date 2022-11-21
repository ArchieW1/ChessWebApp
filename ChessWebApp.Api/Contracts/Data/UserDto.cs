namespace ChessWebApp.Api.Contracts.Data;

public sealed class UserDto
{
    public string Username { get; init; } = default!;
    
    public string Email { get; init; } = default!;

    public string Password { get; init; } = default!;

    public string Salt { get; set; } = default!;

    public int Wins { get; init; }

    public int Losses { get; init; }
}