using ChessWebApp.ChessEngine.Boardd;

namespace ChessWebApp.ChessEngine.Pieces;

public class Rook : DirectionalPiece
{
    public Rook(int position, Alliance alliance) : base(position, alliance)
    {
        CandidateMoveVectorTransformations = new [] {-9, -8, -7, -1, 1, 7, 8, 9};
    }

    protected override bool IsExclusion(int currentPosition, int transformation)
    {
        return BoardUtils.CoordinatesColumn(currentPosition) switch
        {
            Column.First => transformation is -1,
            Column.Eighth => transformation is 1,
            _ => false
        };
    }
}