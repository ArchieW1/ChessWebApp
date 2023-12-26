using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;

namespace ChessWebApp.ChessEngine.PieceLib;

public abstract class Piece
{
    public Alliance Alliance { get; }
    public int Position { get; }
    public bool IsFirstMove { get; protected init; }
    protected string Symbol { private get; init; } = string.Empty;
    public int PieceValue { get; protected init; } = 0;

    protected Piece(int position, Alliance alliance, bool isFirstMove)
    {
        Position = position;
        Alliance = alliance;
        IsFirstMove = isFirstMove;
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