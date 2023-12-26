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
            @"INSERT INTO UserLogin
              (
                  Password,
                  Salt,
                  Email
              )
              VALUES
              (@Password, @Salt, @Email);

              INSERT INTO UserStats
              (
                  Wins,
                  Losses
              )
              VALUES
              (@Wins, @Losses);
              
              INSERT INTO Users
              (
                  Username,
                  UserLoginId,
                  UserStatsId
              )
              VALUES
              (@Username, (SELECT max(Id) FROM UserLogin), (SELECT max(Id) FROM UserStats));",
            user);
        
        return result >= 3;
    }

    public async Task<UserDto?> GetAsync(string username)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<UserDto>(
            @"SELECT *
              FROM Users 
                INNER JOIN UserLogin ON Users.UserLoginId = UserLogin.Id
                INNER JOIN UserStats ON Users.UserStatsId = UserStats.Id
              WHERE
                Username = @Username;",
            new { Username = username });
    }
    
    public async Task<UserDto?> GetStatsAsync(string username)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<UserDto>(
            @"SELECT *
              FROM Users 
                INNER JOIN UserStats ON Users.UserStatsId = UserStats.Id
              WHERE
                Username = @Username;",
            new { Username = username });
    }
    
    public async Task<UserDto?> GetLoginAsync(string username)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<UserDto>(
            @"SELECT *
              FROM Users 
                INNER JOIN UserLogin ON Users.UserLoginId = UserLogin.Id
              WHERE
                Username = @Username;",
            new { Username = username });
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<UserDto>(
            @"SELECT *
              FROM Users 
                INNER JOIN UserLogin ON Users.UserLoginId = UserLogin.Id
                INNER JOIN UserStats ON Users.UserStatsId = UserStats.Id");
    }
    
    public async Task<IEnumerable<UserDto>> GetAllStatsAsync()
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<UserDto>(
            @"SELECT *
              FROM Users 
                INNER JOIN UserStats ON Users.UserStatsId = UserStats.Id");
    }
    
    public async Task<IEnumerable<UserDto>> GetAllLoginAsync()
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<UserDto>(
            @"SELECT *
              FROM Users 
                INNER JOIN UserLogin ON Users.UserLoginId = UserLogin.Id");
    }

    public async Task<bool> UpdateAsync(UserDto user)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        int result = await connection.ExecuteAsync(
            @"UPDATE Users
              SET
                 Username = @Username, Password = @Password, Email = @Email, Wins = @Wins, Losses = @Losses
              FROM Users
                INNER JOIN UserLogin ON Users.UserLoginId = UserLogin.Id
                INNER JOIN UserStats ON Users.UserStatsId = UserStats.Id
              WHERE
                 Username = @Username", 
            user);
        return result > 0;
    }
    
    public async Task<bool> UpdateStatsAsync(UserDto user)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        int result = await connection.ExecuteAsync(
            @"UPDATE Users
              SET
                 Username = @Username, Wins = @Wins, Losses = @Losses
              FROM Users
                INNER JOIN UserStats ON Users.UserStatsId = UserStats.Id
              WHERE
                 Username = @Username", 
            new { user.Username, user.Wins, user.Losses });
        return result > 0;
    }
    
    public async Task<bool> UpdateLoginAsync(UserDto user)
    {
        using IDbConnection connection = await _connectionFactory.CreateConnectionAsync();
        int result = await connection.ExecuteAsync(
            @"INSERT INTO UserLogin
              (
                  Password,
                  Salt,
                  Email
              )
              VALUES
              (@Password, @Salt, @Email);

              UPDATE Users
              SET
                UserLoginId = (SELECT max(Id) FROM UserLogin)
              WHERE Username = @Username;",
            new { user.Username, user.Password, user.Email, user.Salt });
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
