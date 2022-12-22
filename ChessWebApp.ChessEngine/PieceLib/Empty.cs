using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;

namespace ChessWebApp.ChessEngine.PieceLib;

public sealed class Empty : Piece
{
    public Empty(int position) : base(position, Alliance.None)
    {
        Symbol = "-";
    }

    public override IEnumerable<Move> CalculateLegalMoves(Board board)
    {
        return Enumerable.Empty<Move>();
    }

    protected override bool IsExclusion(int currentPosition, int transformation)
    {
        return true;
    }
    
    public override Piece MovePiece(Move move)
    {
        return new Empty(Position);
    }
}