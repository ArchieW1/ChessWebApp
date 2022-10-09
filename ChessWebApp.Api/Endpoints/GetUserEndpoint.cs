using ChessWebApp.Api.Contracts.Requests;
using ChessWebApp.Api.Contracts.Responses;
using ChessWebApp.Api.Domain;
using ChessWebApp.Api.Mapping;
using ChessWebApp.Api.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace ChessWebApp.Api.Endpoints;

[HttpGet("users/{id:guid}"), AllowAnonymous]
public sealed class GetUserEndpoint : Endpoint<GetCustomerRequest, UserResponse>
{
    private readonly IUserService _userService;

    public GetUserEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task HandleAsync(GetCustomerRequest req, CancellationToken ct)
    {
        User? user = await _userService.GetAsync(req.Id);

        if (user is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        UserResponse userResponse = user.ToUserResponse();
        await SendOkAsync(userResponse, ct);
    }
}
