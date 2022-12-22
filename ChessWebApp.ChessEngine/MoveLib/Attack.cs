using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.MoveLib;

public class Attack : Move
{
    private Piece _attackedPiece;

    public Attack(Board board, Piece movedPiece, int destinationCoordinate, Piece attackedPiece) :
        base(board, movedPiece, destinationCoordinate)
    {
        _attackedPiece = attackedPiece;
    }

    public override Board Execute()
    {
        throw new NotImplementedException();
    }
}