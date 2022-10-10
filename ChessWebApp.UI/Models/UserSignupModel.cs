using System.ComponentModel.DataAnnotations;

namespace ChessWebApp.UI.Models;

public sealed class UserSignupModel
{
    [Required(ErrorMessage = "Please complete field.")]
    [RegularExpression("^([a-zA-Z0-9]+){0,30}$",
        ErrorMessage = "Name must only include letters, digits and 30 characters.")]
    public string Username { get; set; } = default!;
    
    [Required(ErrorMessage = "Please complete field.")]
    [RegularExpression("^[\\w!#$%&’*+/=?`{|}~^-]+(?:\\.[\\w!#$%&’*+/=?`{|}~^-]+)*@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$",
        ErrorMessage = "Email is not valid.")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Please complete field.")]
    [MinLength(5, ErrorMessage = "Password must be a minimum of 5 characters.")]
    public string Password { get; set; } = default!;
}