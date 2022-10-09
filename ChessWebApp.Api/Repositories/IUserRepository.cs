using ChessWebApp.Shared.Contracts;

namespace ChessWebApp.Api.Repositories;

public interface IUserRepository
{
    Task<bool> CreateAsync(UserDto user);

    Task<UserDto?> GetAsync(string username);

    Task<IEnumerable<UserDto>> GetAllAsync();

    Task<bool> UpdateAsync(UserDto user);

    Task<bool> DeleteAsync(string username);
}
