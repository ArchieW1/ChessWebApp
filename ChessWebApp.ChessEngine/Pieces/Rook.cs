using ChessWebApp.ChessEngine.BoardLib;

namespace ChessWebApp.ChessEngine.Pieces;

public sealed class Rook : DirectionalPiece
{
    public Rook(int position, Alliance alliance) : base(position, alliance)
    {
        CandidateMoveVectorTransformations = new [] {-9, -8, -7, -1, 1, 7, 8, 9};
        Symbol = "R";
    }

    protected override bool IsExclusion(int currentPosition, int transformation)
    {
        return currentPosition.ToColumn() switch
        {
            Board.Utils.Column.First => transformation is -1,
            Board.Utils.Column.Eighth => transformation is 1,
            _ => false
        };
    }
}