using ChessWebApp.UI.Models;
using Microsoft.AspNetCore.Components;

namespace ChessWebApp.UI.Components;

public sealed partial class SignupForm : ComponentBase
{
    [Parameter]
    public bool IsConnected { get; set; }
    
    private readonly UserSignupModel _userSignupModel = new();
    private bool _signupFailed;
	
    private async Task OnValidSignupSubmitAsync()
    {
        HttpResponseMessage response = await _userService.CreateAsync(new UserModel
        {
            Username = _userSignupModel.Username,
            Email = _userSignupModel.Email,
            Password = _userSignupModel.Password
        });

        if (!response.IsSuccessStatusCode)
        {
            _signupFailed = true;
        }
    }
}