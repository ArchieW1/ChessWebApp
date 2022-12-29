using ChessWebApp.UI.Models;

namespace ChessWebApp.UI.Services;

public interface IBoardService
{
    public int NumberOfTiles { get; }
    public int NumberOfColumns { get; }
    public void MovePiece();
}