using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.MoveLib;

public abstract class Castle : Move
{
    public Rook Rook { get; }
    protected int RookStart { get; }
    protected int RookDestination { get; }

    protected Castle(Board board, Piece movedPiece, int destinationCoordinate, Rook rook, int rookStart,
        int rookDestination) : base(board, movedPiece, destinationCoordinate, true)
    {
        Rook = rook;
        RookStart = rookStart;
        RookDestination = rookDestination;
    }

    public override Board Execute()
    {
        KeepStateExcludingPieces(MovedPiece, Rook);
        
        Board.Builder.SetPiece(MovedPiece.MovePiece(this));
        Board.Builder.SetPiece(new Rook(RookDestination, Rook.Alliance));
        Board.Builder.SetMoveMaker(Board.CurrentPlayer.Opponent.Alliance);
        return Board.Builder.Build();
    }
}