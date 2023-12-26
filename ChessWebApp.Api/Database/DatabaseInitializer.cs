using System.Data;
using Dapper;

namespace ChessWebApp.Api.Database;

public sealed class DatabaseInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DatabaseInitializer(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeAsync()
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();

        await connection.ExecuteAsync(
            @"CREATE TABLE IF NOT EXISTS UserLogin
              (
                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                  Email TEXT NOT NULL, 
                  Password TEXT NOT NULL, 
                  Salt TEXT NOT NULL
              );

              CREATE TABLE IF NOT EXISTS UserStats
              (
                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                  Wins INTEGER NOT NULL, 
                  Losses INTEGER NOT NULL
              );

              CREATE TABLE IF NOT EXISTS Users
              (
                  Username TEXT PRIMARY KEY, 
                  UserLoginId INTEGER NOT NULL,
                  UserStatsId INTEGER NOT NULL,
                  FOREIGN KEY (UserLoginId) REFERENCES UserLogin(Id),
                  FOREIGN KEY (UserStatsId) REFERENCES UserStats(Id)
              );
              
              CREATE TABLE IF NOT EXISTS Games
              (
                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                  Player TEXT NOT NULL,
                  AiDifficulty INTEGER NOT NULL,
                  IsPlayerWinner BIT NOT NULL,
                  FOREIGN KEY (Player) REFERENCES Users(Username)
              );

              CREATE TABLE IF NOT EXISTS Moves
              (
                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                  GameId INTEGER NOT NULL,
                  Player TEXT NOT NULL,
                  Move TEXT NOT NULL,
                  FOREIGN KEY (GameId) REFERENCES Games(Id),
                  FOREIGN KEY (Player) REFERENCES Users(Username)
              );");
    }
}
