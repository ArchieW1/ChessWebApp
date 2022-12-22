using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;

namespace ChessWebApp.ChessEngine.PieceLib;

public abstract class Piece
{
    public Alliance Alliance { get; }
    public int Position { get; }
    public bool IsFirstMove => false;
    protected string Symbol { private get; init; } = string.Empty;

    protected Piece(int position, Alliance alliance)
    {
        Position = position;
        Alliance = alliance;
    }
    
    public override string ToString()
    {
        if (Alliance == Alliance.Black)
        {
            return Symbol.ToLower();
        }

        return Symbol;
    }
    
    public abstract IEnumerable<Move> CalculateLegalMoves(Board board);
    protected abstract bool IsExclusion(int currentPosition, int transformation);
    public abstract Piece MovePiece(Move move);
}