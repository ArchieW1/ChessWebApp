using System.Data;
using ChessWebApp.Api.Contracts.Data;
using ChessWebApp.Api.Database;
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
                    Id,
                    Username,
                    Password,
                    Email,
                    Wins,
                    Losses
                )
                VALUES
                (@Id, @Username, @Password, @Email, @Wins, @Losses)",
            user);
        return result > 0;
    }

    public async Task<UserDto?> GetAsync(Guid id)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<UserDto>(
           @"SELECT
                   * 
                FROM
                   Users 
                WHERE
                   Id = @Id LIMIT 1",
            new { Id = id.ToString() });
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
                   Id = @Id",
            user);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        int result = await connection.ExecuteAsync(
           @"DELETE
                FROM
                   Users 
                WHERE
                   Id = @Id",
            new {Id = id.ToString()});
        return result > 0;
    }
}
