using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.UI.Services;

public sealed class BoardService : IBoardService
{
    public Board Board { get; set; } = Board.CreateStandardBoard();
    public Tile? SourceTile { get; set; }
    public Tile? DestinationTile { get; set; }
    public Piece? HumanMovedPiece { get; set; }
    public bool IsAiGame { get; set; } = false;
}