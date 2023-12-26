using ChessWebApp.Api.Contracts.Requests;
using ChessWebApp.Api.Contracts.Responses;
using ChessWebApp.Api.Domain;
using ChessWebApp.Api.Mapping;
using ChessWebApp.Api.Services;
using FastEndpoints;

namespace ChessWebApp.Api.Endpoints;

[HttpPut("users/{username}")]
public sealed class UpdateUserEndpoint : Endpoint<UpdateUserRequest, UserResponse>
{
    private readonly IUserService _userService;

    public UpdateUserEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task HandleAsync(UpdateUserRequest req, CancellationToken ct)
    {
        User? existingUser = await _userService.GetAsync(req.Username);

        if (existingUser is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        User user = req.ToUser();
        await _userService.UpdateAsync(user);

        UserResponse userResponse = user.ToUserResponse();
        await SendOkAsync(userResponse, ct);
    }
}

[HttpPut("users/login/{username}")]
public sealed class UpdateUserLoginEndpoint : Endpoint<UpdateUserLoginRequest, UserResponse>
{
    private readonly IUserService _userService;

    public UpdateUserLoginEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task HandleAsync(UpdateUserLoginRequest req, CancellationToken ct)
    {
        User? existingUser = await _userService.GetAsync(req.Username);

        if (existingUser is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        User user = req.ToUser();
        await _userService.UpdateLoginAsync(user);

        UserResponse userResponse = user.ToUserResponse();
        await SendOkAsync(userResponse, ct);
    }
}

[HttpPut("users/stats/{username}")]
public sealed class UpdateUserStatsEndpoint : Endpoint<UpdateUserStatsRequest, UserResponse>
{
    private readonly IUserService _userService;

    public UpdateUserStatsEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task HandleAsync(UpdateUserStatsRequest req, CancellationToken ct)
    {
        User? existingUser = await _userService.GetAsync(req.Username);

        if (existingUser is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        User user = req.ToUser();
        await _userService.UpdateStatsAsync(user);

        UserResponse userResponse = user.ToUserResponse();
        await SendOkAsync(userResponse, ct);
    }
}