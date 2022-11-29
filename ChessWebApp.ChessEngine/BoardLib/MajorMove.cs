using ChessWebApp.ChessEngine.Pieces;

namespace ChessWebApp.ChessEngine.BoardLib;

public class MajorMove : Move
{
    public MajorMove(Board board, Piece movedPiece, int destinationCoordinate) 
        : base(board, movedPiece, destinationCoordinate)
    {
    }
}