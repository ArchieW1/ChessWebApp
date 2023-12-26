using System.ComponentModel.DataAnnotations;

namespace ChessWebApp.UI.Models;

public sealed class UserSignupModel
{
    [Required(ErrorMessage = "Please complete field.")]
    [MaxLength(30)]
    public string Username { get; set; } = default!;
    
    [Required(ErrorMessage = "Please complete field.")]
    [EmailAddress(ErrorMessage = "Email is not valid.")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Please complete field.")]
    [MinLength(5, ErrorMessage = "Password must be a minimum of 5 characters.")]
    public string Password { get; set; } = default!;
}