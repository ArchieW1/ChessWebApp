namespace ChessWebApp.Api.Contracts.Requests;

public sealed class GetUserRequest
{
    public string Username { get; init; } = default!;
}
