using ChessWebApp.ChessEngine.Pieces;

namespace ChessWebApp.ChessEngine.BoardLib;

public sealed class Move
{
    public int DestinationCoordinate { get; }
    public Piece MovedPiece { get; }
    private readonly Board _board;

    public Move(Board board, Piece movedPiece, int destinationCoordinate)
    {
        _board = board;
        MovedPiece = movedPiece;
        DestinationCoordinate = destinationCoordinate;
    }

    public Board Execute()
    {
        Board.Builder.SetPiece(new Empty(MovedPiece.Position));
        Board.Builder.SetPiece(MovedPiece.MovePiece(this));
        
        Board.Builder.SetMoveMaker(_board.CurrentPlayer.Opponent.Alliance);
        return Board.Builder.Build();
    }
}