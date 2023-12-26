using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.MoveLib;

public sealed class EnPassant : Move
{

    public EnPassant(Board board, Piece movedPiece, int destinationCoordinate) 
        : base(board, movedPiece, destinationCoordinate)
    {
    }
    
    public override Board Execute()
    {
        KeepStateExcludingPieces(MovedPiece);
        
        Piece pieceAfterMove = MovedPiece.MovePiece(this);
        int opponentPos = pieceAfterMove.Position - Board.Utils.NumberOfRows * pieceAfterMove.Alliance.GetDirection();
        
        Board.Builder.SetPiece(new Empty(MovedPiece.Position));
        Board.Builder.SetPiece(new Empty(opponentPos));
        Board.Builder.SetPiece(pieceAfterMove);
        Board.Builder.SetMoveMaker(Board.CurrentPlayer.Opponent.Alliance);
        
        return Board.Builder.Build();
    }

    public override Board UnExecute(Piece oldPiece)
    {
        KeepStateExcludingPieces(MovedPiece);
        
        Board.Builder.SetPiece(oldPiece);
        Board.Builder.SetPiece(MovedPiece);
        Board.Builder.SetMoveMaker(Board.CurrentPlayer.Alliance);

        return Board.Builder.Build();
    }
}