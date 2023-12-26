namespace ChessWebApp.Api.Contracts.Responses;

public sealed class LoginUserResponse
{
    public string JwtToken { get; set; } = default!;
}