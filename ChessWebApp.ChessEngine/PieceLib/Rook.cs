using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;

namespace ChessWebApp.ChessEngine.PieceLib;

public sealed class Rook : DirectionalPiece
{
    public Rook(int position, Alliance alliance, bool isFirstMove = true) : base(position, alliance, isFirstMove)
    {
        CandidateMoveVectorTransformations = new [] {-8, -1, 1, 8};
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
    
    public override Rook MovePiece(Move move)
    {
        return new Rook(move.DestinationCoordinate, move.MovedPiece.Alliance);
    }
}