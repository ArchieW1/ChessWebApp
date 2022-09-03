namespace ChessWebApp.ChessLogic;

public class Game
{
    public bool IsWhiteTurn { get; set; }
    public List<PieceBase> Pieces { get; set; } = new();
}