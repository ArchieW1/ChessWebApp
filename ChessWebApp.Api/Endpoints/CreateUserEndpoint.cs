using ChessWebApp.Api.Contracts.Requests;
using ChessWebApp.Api.Contracts.Responses;
using ChessWebApp.Api.Domain;
using ChessWebApp.Api.Mapping;
using ChessWebApp.Api.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace ChessWebApp.Api.Endpoints;

[HttpPost("users"), AllowAnonymous]
public sealed class CreateUserEndpoint : Endpoint<CreateUserRequest, UserResponse>
{
    private readonly IUserService _userService;

    public CreateUserEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        User user = req.ToUser();

        await _userService.CreateAsync(user);

        UserResponse userResponse = user.ToUserResponse();
        await SendCreatedAtAsync<GetUserEndpoint>(
            new { Username = user.Username.Value }, userResponse, generateAbsoluteUrl: true, cancellation: ct);
    }
}
