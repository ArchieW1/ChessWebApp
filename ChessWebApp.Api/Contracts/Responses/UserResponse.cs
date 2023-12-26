namespace ChessWebApp.Api.Contracts.Responses;

public sealed class UserResponse
{
    public string Username { get; init; } = default!;
    
    public string Email { get; init; } = default!;
    
    public int Wins { get; init; }

    public int Losses { get; init; }
}

public sealed class GetUserLoginResponse
{
    public string Username { get; init; } = default!;
    
    public string Email { get; init; } = default!;
}

public sealed class GetUserStatsResponse
{
    public string Username { get; init; } = default!;
    
    public int Wins { get; init; }

    public int Losses { get; init; }
}
