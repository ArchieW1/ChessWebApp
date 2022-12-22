using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;

namespace ChessWebApp.ChessEngine.PieceLib;

public sealed class Bishop : DirectionalPiece
{
    public Bishop(int position, Alliance alliance) : base(position, alliance)
    {
        CandidateMoveVectorTransformations = new [] {-9, -7, 7, 9};
        Symbol = "B";
    }

    protected override bool IsExclusion(int currentPosition, int transformation)
    {
        return currentPosition.ToColumn() switch
        {
            Board.Utils.Column.First => transformation is -9 or 7,
            Board.Utils.Column.Eighth => transformation is -7 or 9,
            _ => false
        };
    }

    public override Piece MovePiece(Move move)
    {
        return new Bishop(move.DestinationCoordinate, move.MovedPiece.Alliance);
    }
}