namespace ChessWebApp.Api.Contracts.Requests;

public sealed class UpdateUserRequest
{
    public string Username { get; init; } = default!;

    public string Email { get; init; } = default!;

    public string Password { get; init; } = default!;

    public int Wins { get; init; } = default!;

    public int Losses { get; set; } = default!;
}
