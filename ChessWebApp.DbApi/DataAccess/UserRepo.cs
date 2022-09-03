using ChessWebApp.DbApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChessWebApp.DbApi.DataAccess;

public class UserRepo : IUserRepo
{
    private readonly AppDbContext _dbContext;

    public UserRepo(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
    
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task CreateAsync(User user)
    {
        await _dbContext.AddAsync(user);
    }

    public void Delete(User user)
    {
        _dbContext.Remove(user);
    }
}