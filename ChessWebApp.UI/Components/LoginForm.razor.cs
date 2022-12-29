using ChessWebApp.UI.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace ChessWebApp.UI.Components;

public sealed partial class LoginForm : ComponentBase
{
    [Parameter]
    public bool IsConnected { get; set; }
    
    private readonly UserLoginModel _userLoginModel = new();
    private bool _loginFailed;

    private async Task OnValidLoginSubmitAsync()
    {
        HttpResponseMessage response = await _userService.LoginAsync(new UserModel
        {
            Username = _userLoginModel.Username,
            Password = _userLoginModel.Password
        });

        if (!response.IsSuccessStatusCode)
        {
            _loginFailed = true;
            return;
        }
        
        string stringJwt = await response.Content.ReadAsStringAsync();
        Jwt jwt = JsonConvert.DeserializeObject<Jwt>(stringJwt);
        await _browserStorage.SetItemAsync("token", jwt.JwtToken);
        await _browserStorage.SetItemAsync("username", _userLoginModel.Username);
        _navManager.NavigateTo("/");
    }
}