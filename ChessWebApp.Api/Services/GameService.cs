using ChessWebApp.Api.Repositories;

namespace ChessWebApp.Api.Services;

public sealed class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }
    
    public async Task<string> GetFavouriteMove(string player)
    {
        string? favouriteMove = await _gameRepository.GetFavouriteMove(player);
        return favouriteMove ?? "None";
    }
    
    public async Task<double> GetUserWinRate(string player)
    {
        double winRate = await _gameRepository.GetUserWinRate(player);
        return winRate;
    }
}