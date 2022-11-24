using ChessWebApp.ChessEngine.Pieces;

namespace ChessWebApp.ChessEngine.Boardd;

public class AttackMove : Move
{
    private Piece _attackedPiece;
    
    public AttackMove(Board board, Piece movedPiece, int destinationCoordinate, Piece attackedPiece)
        : base(board, movedPiece, destinationCoordinate)
    {
        _attackedPiece = attackedPiece;
    }
}