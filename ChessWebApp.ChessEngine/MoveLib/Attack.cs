using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.MoveLib;

public class Attack : Move
{
    public Piece AttackedPiece { get; }
    public bool IsAttack => true;

    public Attack(Board board, Piece movedPiece, int destinationCoordinate, Piece attackedPiece) :
        base(board, movedPiece, destinationCoordinate)
    {
        AttackedPiece = attackedPiece;
    }

    public override Board Execute()
    {
        return base.Execute();
    }
}