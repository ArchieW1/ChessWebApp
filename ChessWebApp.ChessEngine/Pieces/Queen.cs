using ChessWebApp.ChessEngine.BoardLib;

namespace ChessWebApp.ChessEngine.Pieces;

public sealed class Queen : DirectionalPiece
{
    public Queen(int position, Alliance alliance) : base(position, alliance)
    {
        CandidateMoveVectorTransformations = new[] {-9, -7, 7, 9};
        Symbol = "Q";
    }

    protected override bool IsExclusion(int currentPosition, int transformation)
    {
        return currentPosition.ToColumn() switch
        {
            Board.Utils.Column.First => transformation is -9 or -1 or 7,
            Board.Utils.Column.Eighth => transformation is -7 or 1 or 9,
            _ => false
        };
    }
    
    public override Piece MovePiece(Move move)
    {
        return new Queen(move.DestinationCoordinate, move.MovedPiece.Alliance);
    }
}