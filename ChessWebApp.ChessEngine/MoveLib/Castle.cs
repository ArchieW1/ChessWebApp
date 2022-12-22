using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.MoveLib;

public abstract class Castle : Move
{
    protected Castle(Board board, Piece movedPiece, int destinationCoordinate) :
        base(board, movedPiece, destinationCoordinate)
    {
    }
}