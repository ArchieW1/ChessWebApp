namespace ChessWebApp.Api.Contracts.Data;

public sealed class GameDto
{
    public string Player { get; set; } = default!;
    
    public int AiDifficulty { get; set; }
    
    public string Winner { get; set; } = default!;
}