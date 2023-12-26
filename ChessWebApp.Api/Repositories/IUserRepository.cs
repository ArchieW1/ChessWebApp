using ChessWebApp.Api.Contracts.Data;

namespace ChessWebApp.Api.Repositories;

public interface IUserRepository
{
    Task<bool> CreateAsync(UserDto user);

    Task<UserDto?> GetAsync(string username);
    
    Task<UserDto?> GetLoginAsync(string username);
    
    Task<UserDto?> GetStatsAsync(string username);

    Task<IEnumerable<UserDto>> GetAllAsync();
    
    Task<IEnumerable<UserDto>> GetAllStatsAsync();
    
    Task<IEnumerable<UserDto>> GetAllLoginAsync();

    Task<bool> UpdateAsync(UserDto user);
    
    Task<bool> UpdateLoginAsync(UserDto user);
    
    Task<bool> UpdateStatsAsync(UserDto user);

    Task<bool> DeleteAsync(string username);
}
