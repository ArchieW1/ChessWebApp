namespace ChessWebApp.UI.Models;

public sealed class UserModel
{
    public string Username { get; set; } = default!;
    
    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;

    public int Wins { get; set; }

    public int Losses { get; set; }
}