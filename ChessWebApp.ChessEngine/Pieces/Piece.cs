using System.Collections.ObjectModel;
using ChessWebApp.ChessEngine.BoardLib;

namespace ChessWebApp.ChessEngine.Pieces;

public abstract class Piece
{
    public Alliance Alliance { get; }
    public int Position { get; }
    protected bool IsFirstMove { get; } = false;

    protected Piece(int position, Alliance alliance)
    {
        Position = position;
        Alliance = alliance;
    }

    public abstract ReadOnlyCollection<Move> CalculateLegalMoves(Board board);
    protected abstract bool IsExclusion(int currentPosition, int transformation);
}