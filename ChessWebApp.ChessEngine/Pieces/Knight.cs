using System.Collections.ObjectModel;
using ChessWebApp.ChessEngine.BoardLib;

namespace ChessWebApp.ChessEngine.Pieces;

public sealed class Knight : Piece
{
    private static readonly int[] CandidateMoveTransformations = {-17, -15, -10, -6, 6, 10, 15, 17};
    
    public Knight(int position, Alliance alliance) : base(position, alliance)
    {
        Symbol = "N";
    }

    public override IEnumerable<Move> CalculateLegalMoves(Board board)
    {
        List<Move> legalMoves = new List<Move>();
        
        foreach (int moveTransformation in CandidateMoveTransformations)
        {
            int destinationCoordinate = Position + moveTransformation;
            if (!destinationCoordinate.IsValid() || IsExclusion(Position, moveTransformation))
            {
                continue;
            }
            
            Tile destinationCoordinateTile = board[destinationCoordinate];
            if (!destinationCoordinateTile.IsTileOccupied)
            {
                legalMoves.Add(new Move(board, this, destinationCoordinate));
                continue;
            }
            
            Alliance pieceAlliance = destinationCoordinateTile.Piece!.Alliance;
            if (Alliance != pieceAlliance)
            {
                legalMoves.Add(new Move(board, this, destinationCoordinate));
            }
        }
        
        return new ReadOnlyCollection<Move>(legalMoves);
    }

    protected override bool IsExclusion(int currentPosition, int transformation)
    {
        return currentPosition.GetColumn() switch
        {
            BoardUtils.Column.First => transformation is -17 or -10 or 6 or 15,
            BoardUtils.Column.Second => transformation is -10 or 6,
            BoardUtils.Column.Seventh => transformation is -6 or 10,
            BoardUtils.Column.Eighth => transformation is -15 or -6 or 10 or 17,
            _ => false
        };
    }
}