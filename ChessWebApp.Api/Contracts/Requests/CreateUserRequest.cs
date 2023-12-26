namespace ChessWebApp.Api.Contracts.Requests;

public sealed class CreateUserRequest
{
    public string Username { get; init; } = default!;
    
    public string Email { get; init; } = default!;

    public string Password { get; init; } = default!;
}
