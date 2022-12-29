using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.UI.Models;

namespace ChessWebApp.UI.Services;

public sealed class BoardService : IBoardService
{
    private Board _board = Board.CreateStandardBoard();

    public int NumberOfTiles => Board.Utils.NumberOfTiles;
    public int NumberOfColumns => Board.Utils.NumberOfColumns;
    
    public void MovePiece()
    {
        
    }
}