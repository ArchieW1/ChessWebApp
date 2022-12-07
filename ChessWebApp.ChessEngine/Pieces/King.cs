using System.Collections.ObjectModel;
using ChessWebApp.ChessEngine.BoardLib;

namespace ChessWebApp.ChessEngine.Pieces;

public sealed class King : Piece
{
    private static readonly int[] CandidateMoveTransformations = {-9, -8, -7, -1, 1, 7, 8, 9};
    
    public King(int position, Alliance alliance) : base(position, alliance)
    {
        Symbol = "K";
    }
   
    public override IEnumerable<Move> CalculateLegalMoves(Board board)
    {
        List<Move> legalMoves = new();

        foreach (int moveTransformation in CandidateMoveTransformations)
        {
            int destinationCoordinate = Position + moveTransformation;

            if (!BoardUtils.IsValidCoordinate(destinationCoordinate) || IsExclusion(Position, moveTransformation))
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
        return BoardUtils.CoordinatesColumn(currentPosition) switch
        {
            Column.First => transformation is -9 or -1 or 7,
            Column.Eighth => transformation is -7 or 1 or 9,
            _ => false
        };
    }
}