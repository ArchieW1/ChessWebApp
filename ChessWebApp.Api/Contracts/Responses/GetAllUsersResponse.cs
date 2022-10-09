namespace ChessWebApp.Api.Contracts.Responses;

public sealed class GetAllUsersResponse
{
    public IEnumerable<UserResponse> Customers { get; init; } = Enumerable.Empty<UserResponse>();
}
