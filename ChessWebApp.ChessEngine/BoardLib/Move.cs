using ChessWebApp.ChessEngine.Pieces;

namespace ChessWebApp.ChessEngine.BoardLib;

public sealed class Move
{
    private Board _board;
    private Piece _movedPiece;
    private int _destinationCoordinate;

    public Move(Board board, Piece movedPiece, int destinationCoordinate)
    {
        _board = board;
        _movedPiece = movedPiece;
        _destinationCoordinate = destinationCoordinate;
    }
}