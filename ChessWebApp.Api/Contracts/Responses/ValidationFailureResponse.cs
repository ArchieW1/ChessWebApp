namespace ChessWebApp.Api.Contracts.Responses;

public sealed class ValidationFailureResponse
{
    public List<string> Errors { get; init; } = new();
}
