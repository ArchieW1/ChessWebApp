using ChessWebApp.Api.Contracts.Requests;
using ChessWebApp.Api.Contracts.Responses;
using ChessWebApp.Api.Domain;
using ChessWebApp.Api.Mapping;
using ChessWebApp.Api.Services;
using FastEndpoints;

namespace ChessWebApp.Api.Endpoints;

[HttpGet("users/{username}")]
public sealed class GetUserEndpoint : Endpoint<GetUserRequest, UserResponse>
{
    private readonly IUserService _userService;

    public GetUserEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
    {
        User? user = await _userService.GetAsync(req.Username);

        if (user is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        UserResponse userResponse = user.ToUserResponse();
        await SendOkAsync(userResponse, ct);
    }
}

[HttpGet("users/login/{username}")]
public sealed class GetUserLoginEndpoint : Endpoint<GetUserRequest, GetUserLoginResponse>
{
    private readonly IUserService _userService;

    public GetUserLoginEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
    {
        User? user = await _userService.GetAsync(req.Username);

        if (user is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        GetUserLoginResponse userResponse = user.ToUserLoginResponse();
        await SendOkAsync(userResponse, ct);
    }
}

[HttpGet("users/stats/{username}")]
public sealed class GetUserStatsEndpoint : Endpoint<GetUserRequest, GetUserStatsResponse>
{
    private readonly IUserService _userService;

    public GetUserStatsEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
    {
        User? user = await _userService.GetAsync(req.Username);

        if (user is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        GetUserStatsResponse userResponse = user.ToUserStatsResponse();
        await SendOkAsync(userResponse, ct);
    }
}