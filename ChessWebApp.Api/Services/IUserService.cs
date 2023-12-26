using ChessWebApp.Api.Domain;

namespace ChessWebApp.Api.Services;

public interface IUserService
{
    Task<bool> CreateAsync(User user);

    Task<User?> GetAsync(string username);
    
    Task<User?> GetLoginAsync(string username);
    
    Task<User?> GetStatsAsync(string username);

    Task<IEnumerable<User>> GetAllAsync();
    
    Task<IEnumerable<User>> GetAllStatsAsync();
    
    Task<IEnumerable<User>> GetAllLoginAsync();

    Task<bool> UpdateAsync(User user);
    
    Task<bool> UpdateLoginAsync(User user);
    
    Task<bool> UpdateStatsAsync(User user);

    Task<bool> DeleteAsync(string username);
}
