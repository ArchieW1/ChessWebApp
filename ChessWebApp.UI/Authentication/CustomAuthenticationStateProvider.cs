using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using ProtectedLocalStore;

namespace ChessWebApp.UI.Authentication;

public sealed class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedLocalStorage _localStorage;
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public CustomAuthenticationStateProvider(ProtectedLocalStorage localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            UserSession? userSession = await _localStorage.GetItemAsync<UserSession>("UserSession");
            if (userSession is null)
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }

            ClaimsPrincipal claimsPrinciple = new(new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.Name, userSession.Username),
                new(ClaimTypes.Role, userSession.Role)
            }, "CustomAuth"));
            return await Task.FromResult(new AuthenticationState(claimsPrinciple));
        }
        catch
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task UpdateAuthenticationState(UserSession? userSession)
    {
        ClaimsPrincipal claimsPrincipal;

        if (userSession is not null)
        {
            await _localStorage.SetItemAsync("UserSession", userSession);
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.Name, userSession.Username),
                new(ClaimTypes.Role, userSession.Role),
            }));
        }
        else
        {
            await _localStorage.RemoveItemAsync("UserSession");
            claimsPrincipal = _anonymous;
        }
        
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
}