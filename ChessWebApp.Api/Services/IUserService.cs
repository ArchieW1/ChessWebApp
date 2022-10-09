using ChessWebApp.Api.Domain;

namespace ChessWebApp.Api.Services;

public interface IUserService
{
    Task<bool> CreateAsync(User user);

    Task<User?> GetAsync(string username);

    Task<IEnumerable<User>> GetAllAsync();

    Task<bool> UpdateAsync(User user);

    Task<bool> DeleteAsync(string username);
}
