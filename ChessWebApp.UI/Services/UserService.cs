using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using ChessWebApp.UI.Models;

namespace ChessWebApp.UI.Services;

public sealed class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _browserStorage;

    public UserService(HttpClient httpClient, ILocalStorageService browserStorage)
    {
        _httpClient = httpClient;
        _browserStorage = browserStorage;
    }

    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        await SetTokenToHeader();
        return await _httpClient.GetFromJsonAsync<IEnumerable<UserModel>>("users") ?? new List<UserModel>();
    }

    public async Task<UserModel> GetAsync(string name)
    {
        await SetTokenToHeader();
        return await _httpClient.GetFromJsonAsync<UserModel>($"users/{name}") ?? default!;
    }

    public async Task<HttpResponseMessage> CreateAsync(UserModel user)
    {
        return await _httpClient.PostAsJsonAsync("users", user);
    }

    public async Task<HttpResponseMessage> UpdateAsync(UserModel user)
    {
        await SetTokenToHeader();
        return await _httpClient.PutAsJsonAsync("users", user);
    }

    public async Task<HttpResponseMessage> DeleteAsync(string name)
    {
        await SetTokenToHeader();
        return await _httpClient.DeleteAsync($"users/{name}");
    }

    public async Task<HttpResponseMessage> LoginAsync(UserModel user)
    {
        return await _httpClient.PostAsJsonAsync("users/login", user);
    }

    private async Task SetTokenToHeader()
    {
        string jwtToken = await _browserStorage.GetItemAsync<string>("token");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
    }
}