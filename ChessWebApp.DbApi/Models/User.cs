using System.ComponentModel.DataAnnotations;

namespace ChessWebApp.DbApi.Models;

public sealed class User
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string? Username { get; set; }
    
    [Required]
    public string? Email { get; set; }
    
    [Required]
    public string? Password { get; set; }
}