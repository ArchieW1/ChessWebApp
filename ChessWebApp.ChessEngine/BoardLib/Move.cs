using ChessWebApp.ChessEngine.Pieces;

namespace ChessWebApp.ChessEngine.BoardLib;

public abstract class Move
{
    protected Board Board { get; }
    protected Piece MovedPiece { get; }
    protected int DestinationCoordinate { get; set; }

    protected Move(Board board, Piece movedPiece, int destinationCoordinate)
    {
        Board = board;
        MovedPiece = movedPiece;
        DestinationCoordinate = destinationCoordinate;
    }
}