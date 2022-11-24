using ChessWebApp.ChessEngine.Boardd;

namespace ChessWebApp.ChessEngine.Pieces;

public class Bishop : DirectionalPiece
{
    public Bishop(int position, Alliance alliance) : base(position, alliance)
    {
        CandidateMoveVectorTransformations = new [] {-9, -7, 7, 9};
    }

    protected override bool IsExclusion(int currentPosition, int transformation)
    {
        return BoardUtils.CoordinatesRow(currentPosition) switch
        {
            Row.First => transformation is -9 or 7,
            Row.Eighth => transformation is -7 or 9,
            _ => false
        };
    }
}