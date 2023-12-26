using ChessWebApp.Api.Contracts.Data;

namespace ChessWebApp.Api.Repositories;

public interface IGameRepository
{
    Task<bool> CreateAsync(GameDto game);
    
    Task<bool> CreateMovesAsync(IEnumerable<MoveDto> moves);
    
    Task<GameDto?> GetAsync(int id);
    
    Task<IEnumerable<MoveDto>> GetMovesAsync(int gameId);
    
    Task<IEnumerable<MoveDto>> GetMovesAsync(string player);
    
    Task<IEnumerable<GameDto>> GetAllAsync(string username);
    
    Task<bool> DeleteAsync(int id);

    Task<string?> GetFavouriteMove(string player);
    
    Task<double> GetUserWinRate(string player);
}