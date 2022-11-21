using ChessWebApp.Api.Contracts.Requests;
using ChessWebApp.Api.Services;
using FastEndpoints;

namespace ChessWebApp.Api.Endpoints;

[HttpDelete("users/{username}")]
public sealed class DeleteUserEndpoint : Endpoint<DeleteUserRequest>
{
    private readonly IUserService _userService;

    public DeleteUserEndpoint(IUserService userService)
    {
        _userService = userService;
    }

    public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
    {
        bool deleted = await _userService.DeleteAsync(req.Username);
        if (!deleted)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendNoContentAsync(ct);
    }
}
