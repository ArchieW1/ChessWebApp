using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;

namespace ChessWebApp.ChessEngine.PieceLib;

public sealed class Rook : DirectionalPiece
{
    public Rook(int position, Alliance alliance, bool isFirstMove = true) : base(position, alliance, isFirstMove)
    {
        CandidateMoveVectorTransformations = new [] {-8, -1, 1, 8};
        Symbol = "R";
        PieceValue = 500;
    }

    protected override bool IsExclusion(int currentPosition, int transformation)
    {
        return currentPosition.ToRow() switch
        {
            Board.Utils.Row.First => transformation is -1,
            Board.Utils.Row.Eighth => transformation is 1,
            _ => false
        };
    }
    
    public override Rook MovePiece(Move move)
    {
        return new Rook(move.DestinationCoordinate, move.MovedPiece.Alliance, false);
    }
}