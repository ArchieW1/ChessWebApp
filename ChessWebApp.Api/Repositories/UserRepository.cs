using System.Data;
using ChessWebApp.Api.Contracts.Data;
using ChessWebApp.Api.Database;
using ChessWebApp.Api.Domain.Common;
using Dapper;

namespace ChessWebApp.Api.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UserRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> CreateAsync(UserDto user)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        int result = await connection.ExecuteAsync(
           @"INSERT INTO Users
                (
                    Username,
                    Password,
                    Email,
                    Wins,
                    Losses
                )
                VALUES
                (@Username, @Password, @Email, @Wins, @Losses)",
            user);
        return result > 0;
    }

    public async Task<UserDto?> GetAsync(string username)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<UserDto>(
           @"SELECT
                   * 
                FROM
                   Users 
                WHERE
                   Username = @Username LIMIT 1",
            new { Username = username });
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<UserDto>(
           @"SELECT
                   * 
                FROM
                   Users"
            );
    }

    public async Task<bool> UpdateAsync(UserDto user)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        int result = await connection.ExecuteAsync(
           @"UPDATE
                   Users 
                SET
                   Username = @Username, Password = @Password, Email = @Email, Wins = @Wins, Losses = @Losses
                WHERE
                   Username = @Username",
            user);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(string username)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        int result = await connection.ExecuteAsync(
           @"DELETE
                FROM
                   Users 
                WHERE
                   Username = @Username",
            new {Username = username});
        return result > 0;
    }
}
