using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.MoveLib;

public abstract partial class Move
{
    public int DestinationCoordinate { get; }
    public Piece MovedPiece { get; }
    private readonly Board _board;

    protected Move(Board board, Piece movedPiece, int destinationCoordinate)
    {
        _board = board;
        MovedPiece = movedPiece;
        DestinationCoordinate = destinationCoordinate;
    }

    public virtual Board Execute()
    {
        Board.Builder.SetPiece(new Empty(MovedPiece.Position));
        Board.Builder.SetPiece(MovedPiece.MovePiece(this));
        
        Board.Builder.SetMoveMaker(_board.CurrentPlayer.Opponent.Alliance);
        return Board.Builder.Build();
    }
}