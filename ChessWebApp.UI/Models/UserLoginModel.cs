using System.ComponentModel.DataAnnotations;

namespace ChessWebApp.UI.Models;

public sealed class UserLoginModel
{
    [Required(ErrorMessage = "Please complete field.")]
    public string Username { get; set; } = default!;
    
    [Required(ErrorMessage = "Please complete field.")]
    public string Password { get; set; } = default!;
}