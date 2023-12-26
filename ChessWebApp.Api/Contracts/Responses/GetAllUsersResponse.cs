namespace ChessWebApp.Api.Contracts.Responses;

public sealed class GetAllUsersResponse
{
    public IEnumerable<UserResponse> Users { get; init; } = Enumerable.Empty<UserResponse>();
}

public sealed class GetAllUsersLoginResponse
{
    public IEnumerable<GetUserLoginResponse> Users { get; init; } = Enumerable.Empty<GetUserLoginResponse>();
}

public sealed class GetAllUsersStatsResponse
{
    public IEnumerable<GetUserStatsResponse> Users { get; init; } = Enumerable.Empty<GetUserStatsResponse>();
}