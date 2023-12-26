using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.MoveLib;

public sealed class PawnJump : Move
{
    public PawnJump(Board board, Piece movedPiece, int destinationCoordinate) :
        base(board, movedPiece, destinationCoordinate)
    {
    }
    
    public override Board Execute()
    {
        KeepStateExcludingPieces(MovedPiece);

        if (MovedPiece is not Pawn pawn)
        {
            throw new ArgumentException("Piece has to be a pawn");
        }

        Pawn movedPawn = pawn.MovePiece(this);
        
        Board.Builder.SetPiece(new Empty(MovedPiece.Position));
        Board.Builder.SetPiece(movedPawn);
        Board.Builder.SetEnPassantPawn(movedPawn);
        Board.Builder.SetMoveMaker(Board.CurrentPlayer.Opponent.Alliance);
        
        return Board.Builder.Build();
    }
}