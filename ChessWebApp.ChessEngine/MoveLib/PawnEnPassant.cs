using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.MoveLib;

public sealed class PawnEnPassant : PawnAttack
{
    public PawnEnPassant(Board board, Piece movedPiece, int destinationCoordinate, Piece attackedPiece) :
        base(board, movedPiece, destinationCoordinate, attackedPiece)
    {
    }
}