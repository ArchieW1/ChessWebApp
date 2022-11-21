using ChessWebApp.Api.Authentication;
using ChessWebApp.Api.Contracts.Requests;
using ChessWebApp.Api.Contracts.Responses;
using ChessWebApp.Api.Domain;
using ChessWebApp.Api.Mapping;
using ChessWebApp.Api.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace ChessWebApp.Api.Endpoints;

[HttpPost("users/login"), AllowAnonymous]
public sealed class LoginUserEndpoint : Endpoint<LoginUserRequest, LoginUserResponse>
{
    private readonly IUserService _userService;
    private readonly LoginAuthentication _loginAuthentication;

    public LoginUserEndpoint(IUserService userService, LoginAuthentication loginAuthentication)
    {
        _userService = userService;
        _loginAuthentication = loginAuthentication;
    }

    public override async Task HandleAsync(LoginUserRequest req, CancellationToken ct)
    {
        User? user = await _userService.GetAsync(req.Username);

        if (user is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (user.Password.Value != LoginAuthentication.SaltHashSha256(req.Password, user.Salt.Value))
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        await SendOkAsync(new LoginUserResponse
        {
            JwtToken = _loginAuthentication.GenerateJwt(user)
        },ct);
    }
}