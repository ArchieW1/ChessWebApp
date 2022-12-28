using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.MoveLib;

public sealed class KingSideCastle : Castle
{
    public KingSideCastle(Board board, Piece movedPiece, int destinationCoordinate, Rook rook, int rookStart,
        int rookDestination) : base(board, movedPiece, destinationCoordinate, rook, rookStart, rookDestination)
    {
    }

    public override string ToString()
    {
        return "0-0";
    }
}