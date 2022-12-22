using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.MoveLib;

public sealed class QueenSideCastle : Castle
{
    public QueenSideCastle(Board board, Piece movedPiece, int destinationCoordinate) :
        base(board, movedPiece, destinationCoordinate)
    {
    }
}