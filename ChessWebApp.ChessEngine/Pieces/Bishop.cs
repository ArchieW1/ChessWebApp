using ChessWebApp.ChessEngine.BoardLib;

namespace ChessWebApp.ChessEngine.Pieces;

public sealed class Bishop : DirectionalPiece
{
    public Bishop(int position, Alliance alliance) : base(position, alliance)
    {
        CandidateMoveVectorTransformations = new [] {-9, -7, 7, 9};
        Symbol = "B";
    }

    protected override bool IsExclusion(int currentPosition, int transformation)
    {
        return currentPosition.GetColumn() switch
        {
            BoardUtils.Column.First => transformation is -9 or 7,
            BoardUtils.Column.Eighth => transformation is -7 or 9,
            _ => false
        };
    }
}