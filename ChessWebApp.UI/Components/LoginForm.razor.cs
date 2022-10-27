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
        UserModel? user = await _userService.GetAsync(_userLoginModel.Username);
        if (user is null || user.Password != _userLoginModel.Password)
        {
            _loginFailed = true;
        }
    }
}