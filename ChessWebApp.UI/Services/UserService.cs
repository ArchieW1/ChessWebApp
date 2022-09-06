using ChessWebApp.Shared.Dtos;

namespace ChessWebApp.UI.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<UserReadDto?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserReadDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(UserCreateDto user)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}