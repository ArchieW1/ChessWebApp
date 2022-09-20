namespace ChessWebApp.Shared.Dtos;

public class UserReadPasswordDto
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}