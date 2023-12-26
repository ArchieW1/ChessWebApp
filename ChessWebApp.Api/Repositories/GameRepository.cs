using System.Data;
using ChessWebApp.Api.Contracts.Data;
using ChessWebApp.Api.Database;
using Dapper;

namespace ChessWebApp.Api.Repositories;

public sealed class GameRepository : IGameRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GameRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> CreateAsync(GameDto game)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        int result = await connection.ExecuteAsync(
            @"INSERT INTO Games
              (
                  Player,
                  AiDifficulty,
                  Winner
              )
              VALUES
              (@Player, @AiDifficulty, @Winner);",
            game);

        return result > 0;
    }

    public async Task<bool> CreateMovesAsync(IEnumerable<MoveDto> moves)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        int result = await connection.ExecuteAsync(
            @"INSERT INTO Moves
              (
                  GameId,
                  Player,
                  Move
              )
              VALUES
              (@GameId, @Player, @Move);",
            moves);
        
        return result > 0;
    }

    public async Task<GameDto?> GetAsync(int id)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<GameDto>(
            @"SELECT *
              FROM Games
              WHERE Id = @Id;",
            new {Id = id});
    }

    public async Task<IEnumerable<MoveDto>> GetMovesAsync(int gameId)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<MoveDto>(
            @"SELECT *
              FROM Moves
              WHERE GameId = @GameId;",
            new {GameId = gameId});
    }

    public async Task<IEnumerable<MoveDto>> GetMovesAsync(string player)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<MoveDto>(
            @"SELECT *
              FROM Moves
              WHERE Player = @Player;",
            new {Player = player});
    }

    public async Task<IEnumerable<GameDto>> GetAllAsync(string username)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<GameDto>(
            @"SELECT *
              FROM Games
              WHERE Player = @Player;",
            new {Player = username});
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        int result = await connection.ExecuteAsync(
            @"DELETE FROM Games
              WHERE Id = @Id;",
            new {Id = id});
        
        return result > 0;
    }

    public async Task<string?> GetFavouriteMove(string player)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<string>(
            @"SELECT Move
              FROM Moves
              WHERE Player = @Player
              GROUP BY Move
              ORDER BY COUNT(*) DESC
              LIMIT 1;",
            new {Player = player});
    }
    
    public async Task<double> GetUserWinRate(string player)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<double>(
            @"SELECT COUNT(*) * 100.0 / (SELECT COUNT(*) FROM Games WHERE Player = @Player) AS WinRate
              FROM Games
              WHERE Player = @Player AND Winner = @Player;",
            new {Player = player});
    }
}