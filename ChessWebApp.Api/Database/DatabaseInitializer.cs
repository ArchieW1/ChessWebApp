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
            @"CREATE TABLE IF NOT EXISTS Users
              (
                  Username TEXT PRIMARY KEY, 
                  Email TEXT NOT NULL,
                  Password TEXT NOT NULL, 
                  Salt TEXT NOT NULL,
                  Wins INTEGER NOT NULL,
                  Losses INTEGER NOT NULL
              )");
    }
}
