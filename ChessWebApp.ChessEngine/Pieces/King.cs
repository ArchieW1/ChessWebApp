using System.Collections.ObjectModel;
using ChessWebApp.ChessEngine.BoardLib;

namespace ChessWebApp.ChessEngine.Pieces;

public class King : Piece
{
    private static readonly int[] CandidateMoveTransformations = {-9, -8, -7, -1, 1, 7, 8, 9};
    
    public King(int position, Alliance alliance) : base(position, alliance)
    {
    }

    public override ReadOnlyCollection<Move> CalculateLegalMoves(Board board)
    {
        List<Move> legalMoves = new();

        foreach (int moveTransformation in CandidateMoveTransformations)
        {
            int destinationCoordinate = Position + moveTransformation;

            if (!BoardUtils.IsValidCoordinate(destinationCoordinate) || IsExclusion(Position, moveTransformation))
            {
                continue;
            }
            
            Tile destinationCoordinateTile = board.GetTile(destinationCoordinate);
            if (!destinationCoordinateTile.IsTileOccupied)
            {
                legalMoves.Add(new MajorMove(board, this, destinationCoordinate));
                continue;
            }
            
            Piece pieceAtDestination = destinationCoordinateTile.Piece!;
            Alliance pieceAlliance = pieceAtDestination.Alliance;
            if (Alliance != pieceAlliance)
            {
                legalMoves.Add(new AttackMove(board, this, destinationCoordinate, pieceAtDestination));
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