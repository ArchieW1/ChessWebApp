using ChessWebApp.Api.Contracts.Requests;
using ChessWebApp.Api.Contracts.Responses;
using ChessWebApp.Api.Services;
using FastEndpoints;

namespace ChessWebApp.Api.Endpoints;

[HttpGet("/games/favourite-move")]
public sealed class GetFavouriteMoveEndpoint : Endpoint<GetUserRequest, GetFavouriteMoveResponse>
{
    private readonly IGameService _gameService;

    public GetFavouriteMoveEndpoint(IGameService gameService)
    {
        _gameService = gameService;
    }
    
    public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
    {
        string move = await _gameService.GetFavouriteMove(req.Username);
        await SendOkAsync(new GetFavouriteMoveResponse { Move = move }, ct);
    }
}