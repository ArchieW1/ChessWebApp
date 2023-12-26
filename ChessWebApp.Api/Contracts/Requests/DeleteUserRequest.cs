namespace ChessWebApp.Api.Contracts.Requests;

public sealed class DeleteUserRequest
{
    public string Username { get; init; } = default!;
}
