using ChessWebApp.Api.Contracts.Requests;
using ChessWebApp.Api.Contracts.Responses;
using ChessWebApp.Api.Services;
using FastEndpoints;

namespace ChessWebApp.Api.Endpoints;

[HttpGet("/games/win-rate")]
public sealed class GetWinRateEndpoint : Endpoint<GetUserRequest, GetWinRateResponse>
{
    private readonly IGameService _gameService;

    public GetWinRateEndpoint(IGameService gameService)
    {
        _gameService = gameService;
    }
    
    public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
    {
        double winRate = await _gameService.GetUserWinRate(req.Username);
        await SendOkAsync(new GetWinRateResponse { WinRate = winRate }, ct);
    }
}