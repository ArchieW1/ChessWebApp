using ChessWebApp.Api.Contracts.Responses;
using ChessWebApp.Api.Domain;
using ChessWebApp.Api.Mapping;
using ChessWebApp.Api.Services;
using FastEndpoints;

namespace ChessWebApp.Api.Endpoints;

[HttpGet("users")]
public sealed class GetAllUsersEndpoint : EndpointWithoutRequest<GetAllUsersResponse>
{
    private readonly IUserService _userService;

    public GetAllUsersEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        IEnumerable<User> users = await _userService.GetAllAsync();
        GetAllUsersResponse usersResponse = users.ToUsersResponse();
        await SendOkAsync(usersResponse, ct);
    }
}
