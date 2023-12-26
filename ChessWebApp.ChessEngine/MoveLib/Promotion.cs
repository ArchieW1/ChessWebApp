using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.MoveLib;

public sealed class Promotion : Move
{
    public Pawn PromotedPawn { get; }
    public Move DecoratedMove { get; }
    
    public Promotion(Move decoratedMove) :
        base(decoratedMove.Board, decoratedMove.MovedPiece, decoratedMove.DestinationCoordinate)
    {
        if (decoratedMove.MovedPiece is not Pawn pawn)
        {
            throw new ArgumentException("Decorated move must be a pawn move");
        }

        DecoratedMove = decoratedMove;
        PromotedPawn = pawn;
    }
    
    public override Board Execute()
    {
        DecoratedMove.Execute();
        
        // We will never under-promote.
        // It is statistically proven that 96% of all promotions are to queen.
        Board.Builder.SetPiece(new Queen(DestinationCoordinate, PromotedPawn.Alliance));
        return Board.Builder.Build();
    }
    
    public override Board UnExecute(Piece oldPiece)
    {
        DecoratedMove.UnExecute(oldPiece);
        Board.Builder.SetPiece(PromotedPawn);
        return Board.Builder.Build();
    }
}