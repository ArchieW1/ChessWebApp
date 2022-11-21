using ChessWebApp.UI.Models;

namespace ChessWebApp.UI.Services;

public interface IUserService
{
    public Task<IEnumerable<UserModel>> GetAllAsync();

    public Task<UserModel> GetAsync(string name);

    public Task<HttpResponseMessage> CreateAsync(UserModel user);

    public Task<HttpResponseMessage> UpdateAsync(UserModel user);

    public Task<HttpResponseMessage> DeleteAsync(string name);

    public Task<HttpResponseMessage> LoginAsync(UserModel user);
}