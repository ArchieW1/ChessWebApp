using ChessWebApp.DbApi.Models;

namespace ChessWebApp.DbApi.DataAccess;

public interface IUserRepo
{
    public Task SaveChangesAsync();
    public Task<User?> GetByIdAsync(Guid id);
    public Task<User?> GetByUsernameAsync(string username);
    public Task<User?> GetByUsernamePasswordAsync(string username, string password);
    public Task<User?> GetByEmailAsync(string email);
    public Task<IEnumerable<User>> GetAllAsync();
    public Task CreateAsync(User user);
    public void Delete(User user);
}