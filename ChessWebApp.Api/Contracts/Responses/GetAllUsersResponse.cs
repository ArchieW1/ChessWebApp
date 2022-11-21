namespace ChessWebApp.Api.Contracts.Responses;

public sealed class GetAllUsersResponse
{
    public IEnumerable<UserResponse> Users { get; init; } = Enumerable.Empty<UserResponse>();
}
