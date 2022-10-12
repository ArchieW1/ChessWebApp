using System.Net.Http.Json;
using ChessWebApp.UI.Models;

namespace ChessWebApp.UI.Services;

public sealed class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<UserModel>>("users") ??
               new List<UserModel>();
    }

    public async Task<UserModel?> GetAsync(string name)
    {
        return await _httpClient.GetFromJsonAsync<UserModel>($"users/{name}");
    }

    public async Task<HttpResponseMessage> CreateAsync(UserModel user)
    {
        return await _httpClient.PostAsJsonAsync("users", user);
    }

    public async Task<HttpResponseMessage> UpdateAsync(UserModel user)
    {
        return await _httpClient.PutAsJsonAsync("users", user);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string name)
    {
        return await _httpClient.DeleteAsync($"users/{name}");
    }
}