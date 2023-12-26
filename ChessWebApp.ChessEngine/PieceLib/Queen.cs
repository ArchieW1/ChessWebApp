using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;

namespace ChessWebApp.ChessEngine.PieceLib;

public sealed class Queen : DirectionalPiece
{
    public Queen(int position, Alliance alliance, bool isFirstMove = true) : base(position, alliance, isFirstMove)
    {
        CandidateMoveVectorTransformations = new[] {-9, -8, -7, -1, 1, 7, 8, 9};
        Symbol = "Q";
        PieceValue = 900;
    }

    protected override bool IsExclusion(int currentPosition, int transformation)
    {
        return currentPosition.ToRow() switch
        {
            Board.Utils.Row.First => transformation is -9 or -1 or 7,
            Board.Utils.Row.Eighth => transformation is -7 or 1 or 9,
            _ => false
        };
    }
    
    public override Queen MovePiece(Move move)
    {
        return new Queen(move.DestinationCoordinate, move.MovedPiece.Alliance, false);
    }
}