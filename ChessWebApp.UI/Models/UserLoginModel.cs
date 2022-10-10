using System.ComponentModel.DataAnnotations;

namespace ChessWebApp.UI.Models;

public sealed class UserLoginModel
{
    [Required]
    public string Username { get; set; } = default!;
    
    [Required]
    public string Password { get; set; } = default!;
}