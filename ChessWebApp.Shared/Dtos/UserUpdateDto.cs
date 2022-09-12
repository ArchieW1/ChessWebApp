using System.ComponentModel.DataAnnotations;

namespace ChessWebApp.Shared.Dtos;

public sealed class UserUpdateDto
{
    [Required]
    [MaxLength(20)]
    public string? Username { get; set; }
    
    [Required]
    public string? Email { get; set; }
    
    [Required]
    public string? Password { get; set; }
}