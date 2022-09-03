namespace ChessWebApp.ChessLogic;

public abstract class PieceBase
{
    public string Name { get; set; }
    public int File { get; set; }
    public int Rank { get; set; }
}