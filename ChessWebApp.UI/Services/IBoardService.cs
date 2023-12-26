using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.UI.Services;

public interface IBoardService
{
    public Board Board { get; set; }
    public Tile? SourceTile { get; set; }
    public Tile? DestinationTile { get; set; }
    public Piece? HumanMovedPiece { get; set; }
    public bool IsAiGame { get; set; }
}