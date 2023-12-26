namespace ChessWebApp.Api.Contracts.Data;

public sealed class MoveDto
{
    public int GameId { get; set; }
    
    public string Player { get; set; } = default!;
    
    public string Move { get; set; } = default!;
}