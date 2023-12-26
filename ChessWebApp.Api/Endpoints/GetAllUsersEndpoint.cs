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

[HttpGet("users/login")]
public sealed class GetAllUsersLoginEndpoint : EndpointWithoutRequest<GetAllUsersLoginResponse>
{
    private readonly IUserService _userService;

    public GetAllUsersLoginEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        IEnumerable<User> users = await _userService.GetAllAsync();
        GetAllUsersLoginResponse usersResponse = users.ToUsersLoginResponse();
        await SendOkAsync(usersResponse, ct);
    }
}

[HttpGet("users/stats")]
public sealed class GetAllUsersStatsEndpoint : EndpointWithoutRequest<GetAllUsersStatsResponse>
{
    private readonly IUserService _userService;

    public GetAllUsersStatsEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        IEnumerable<User> users = await _userService.GetAllAsync();
        GetAllUsersStatsResponse usersResponse = users.ToUsersStatsResponse();
        await SendOkAsync(usersResponse, ct);
    }
}