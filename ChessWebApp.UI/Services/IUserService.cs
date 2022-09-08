using ChessWebApp.Shared.Dtos;

namespace ChessWebApp.UI.Services;

public interface IUserService
{
    public Task<UserReadDto?> GetByIdAsync(Guid id);
    public Task<UserReadDto?> GetByUsernameAsync(string username);
    public Task<IEnumerable<UserReadDto>?> GetAllAsync();
    public Task CreateAsync(UserCreateDto user);
    public void Delete(Guid id);
}