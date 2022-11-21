using ChessWebApp.UI.Models;
using Microsoft.AspNetCore.Components;

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

        string content = await response.Content.ReadAsStringAsync();
        Console.WriteLine(content);
    }
}