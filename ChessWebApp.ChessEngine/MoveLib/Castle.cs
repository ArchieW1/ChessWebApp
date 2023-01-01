using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.MoveLib;

public class Castle : Move
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
        
        Board.Builder.SetPiece(MovedPiece.MovePiece(this));
        Board.Builder.SetPiece(new Rook(RookDestination, Rook.Alliance));
        Board.Builder.SetMoveMaker(Board.CurrentPlayer.Opponent.Alliance);
        return Board.Builder.Build();
    }
}