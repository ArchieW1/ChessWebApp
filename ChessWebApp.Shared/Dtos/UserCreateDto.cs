using System.ComponentModel.DataAnnotations;

namespace ChessWebApp.Shared.Dtos;

public class UserCreateDto
{
    [Required]
    [MaxLength(20)]
    public string? Username { get; set; }
    
    [Required]
    public string? Email { get; set; }
    
    [Required]
    public string? Password { get; set; }
}