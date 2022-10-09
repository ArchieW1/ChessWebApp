namespace ChessWebApp.Api.Contracts.Responses;

public sealed class UserResponse
{
    public string Username { get; init; } = default!;
    
    public string Email { get; init; } = default!;

    public string Password { get; set; } = default!;

    public int Wins { get; init; } = default!;

    public int Losses { get; set; } = default!;
}
