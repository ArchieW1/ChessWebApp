using System.Data;
using Microsoft.Data.Sqlite;

namespace ChessWebApp.Api.Database;

public interface IDbConnectionFactory
{
    public Task<IDbConnection> CreateConnectionAsync();
}

public sealed class SqliteConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqliteConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        SqliteConnection connection = new(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}
