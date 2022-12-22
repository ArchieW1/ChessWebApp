using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.MoveLib;

public class PawnAttack : Attack
{
    public PawnAttack(Board board, Piece movedPiece, int destinationCoordinate, Piece attackedPiece) :
        base(board, movedPiece, destinationCoordinate, attackedPiece)
    {
    }
}