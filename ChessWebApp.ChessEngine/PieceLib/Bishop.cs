using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;

namespace ChessWebApp.ChessEngine.PieceLib;

public sealed class Bishop : DirectionalPiece
{
    public Bishop(int position, Alliance alliance, bool isFirstMove = true) : base(position, alliance, isFirstMove)
    {
        CandidateMoveVectorTransformations = new [] {-9, -7, 7, 9};
        Symbol = "B";
        PieceValue = 300;
    }

    protected override bool IsExclusion(int currentPosition, int transformation)
    {
        return currentPosition.ToRow() switch
        {
            Board.Utils.Row.First => transformation is -9 or 7,
            Board.Utils.Row.Eighth => transformation is -7 or 9,
            _ => false
        };
    }

    public override Bishop MovePiece(Move move)
    {
        return new Bishop(move.DestinationCoordinate, move.MovedPiece.Alliance, false);
    }
}