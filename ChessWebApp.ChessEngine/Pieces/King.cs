using System.Collections.ObjectModel;
using ChessWebApp.ChessEngine.BoardLib;

namespace ChessWebApp.ChessEngine.Pieces;

public class King : Piece
{
    public King(int position, Alliance alliance) : base(position, alliance)
    {
    }

    public override ReadOnlyCollection<Move> CalculateLegalMoves(Board board)
    {
        throw new NotImplementedException();
    }

    protected override bool IsExclusion(int currentPosition, int transformation)
    {
        throw new NotImplementedException();
    }
}