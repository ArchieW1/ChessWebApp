using System.Net.Http.Json;
using ChessWebApp.Shared.Dtos;

namespace ChessWebApp.UI.Services;

public sealed class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserReadDto?> GetByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<UserReadDto>($"api/users/id/{id.ToString()}");
    }
    
    public async Task<UserReadDto?> GetByUsernameAsync(string username)
    {
        return await _httpClient.GetFromJsonAsync<UserReadDto>($"api/users/username/{username}");
    }

    public async Task<UserReadPasswordDto?> GetByUsernamePasswordAsync(string username, string password)
    {
        return await _httpClient.GetFromJsonAsync<UserReadPasswordDto>($"api/users/username/{username}/{password}");
    }

    public async Task<IEnumerable<UserReadDto>?> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<UserReadDto>>($"api/users");
    }

    public async Task CreateAsync(UserCreateDto user)
    {
        await _httpClient.PostAsJsonAsync("api/users", user);
    }

    public void Delete(Guid id)
    {
        _httpClient.DeleteAsync($"api/users/{id.ToString()}");
    }
}