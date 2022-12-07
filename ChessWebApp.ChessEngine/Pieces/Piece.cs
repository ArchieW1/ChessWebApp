using ChessWebApp.ChessEngine.BoardLib;

namespace ChessWebApp.ChessEngine.Pieces;

public abstract class Piece
{
    public Alliance Alliance { get; }
    public int Position { get; }
    protected bool IsFirstMove { get; } = false;
    protected string Symbol { private get; set; } = string.Empty;

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
}