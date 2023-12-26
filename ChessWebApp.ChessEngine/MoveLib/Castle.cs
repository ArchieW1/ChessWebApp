using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.MoveLib;

public sealed class Castle : Move
{
    public Rook Rook { get; }
    public int RookStart { get; }
    public int RookDestination { get; }

    public Castle(Board board, Piece movedPiece, int destinationCoordinate, Rook rook, int rookStart,
        int rookDestination) : base(board, movedPiece, destinationCoordinate)
    {
        Rook = rook;
        RookStart = rookStart;
        RookDestination = rookDestination;
    }

    public override Board Execute()
    {
        KeepStateExcludingPieces(MovedPiece, Rook);

        if (MovedPiece is not King king)
        {
            throw new Exception("Must be king to castle.");
        }
        
        Board.Builder.SetPiece(new Empty(MovedPiece.Position));
        Board.Builder.SetPiece(new Empty(Rook.Position));
        Board.Builder.SetPiece(king.MovePiece(this));
        Board.Builder.SetPiece(new Rook(RookDestination, Rook.Alliance));
        Board.Builder.SetMoveMaker(Board.CurrentPlayer.Opponent.Alliance);
        return Board.Builder.Build();
    }

    public override Board UnExecute(Piece oldPiece)
    {
        Board.Builder.SetPiece(MovedPiece);
        Board.Builder.SetPiece(Rook);
        Board.Builder.SetPiece(oldPiece);
        Board.Builder.SetPiece(new Empty(RookDestination));
        Board.Builder.SetMoveMaker(Board.CurrentPlayer.Alliance);
        return Board.Builder.Build();
    }
}