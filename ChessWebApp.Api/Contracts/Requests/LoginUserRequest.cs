namespace ChessWebApp.Api.Contracts.Requests;

public sealed class LoginUserRequest
{
    public string Username { get; init; } = default!;
    
    public string Password { get; init; } = default!;
}