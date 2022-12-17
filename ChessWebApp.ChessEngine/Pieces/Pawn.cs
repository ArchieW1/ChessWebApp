using System.Collections.ObjectModel;
using ChessWebApp.ChessEngine.BoardLib;

namespace ChessWebApp.ChessEngine.Pieces;

public sealed class Pawn : Piece
{
    private static readonly int[] CandidateMoveTransformations = {7, 8, 9, 16};
    
    public Pawn(int position, Alliance alliance) : base(position, alliance)
    {
        Symbol = "P";
    }
    
    public override IEnumerable<Move> CalculateLegalMoves(Board board)
    {
        List<Move> legalMoves = new List<Move>();

        foreach (int moveTransformation in CandidateMoveTransformations)
        {
            int destinationCoordinate = Position + moveTransformation * Alliance.GetDirection();

            if (!BoardUtils.IsValidCoordinate(destinationCoordinate) || IsExclusion(Position, moveTransformation))
            {
                continue;
            }

            Tile destinationTile = board[destinationCoordinate];

            if (moveTransformation is 8 && !destinationTile.IsTileOccupied)
            {
                // TODO- Handle pawn move
                legalMoves.Add(new Move(board, this, destinationCoordinate));
                continue;
            }

            if (IsJumpMove(board, moveTransformation, destinationCoordinate))
            {
                legalMoves.Add(new Move(board, this, destinationCoordinate));
                continue;
            }

            if (moveTransformation is 7 or 9 && destinationTile.IsTileOccupied && 
                destinationTile.Piece!.Alliance == Alliance)
            {
                //TODO- more todo here
                legalMoves.Add(new Move(board, this, destinationCoordinate));
                continue;
            }
            
        }
        
        return new ReadOnlyCollection<Move>(legalMoves);
    }

    private bool IsJumpMove(Board board, int moveTransformation, int destinationCoordinate)
    {
        int behindDestinationCoordinate = Position + Alliance.GetDirection() * 8;
        return 
            (
                (
                    moveTransformation == 16 &&
                    IsFirstMove &&
                    BoardUtils.CoordinatesRow(Position) == BoardUtils.Row.Second &&
                    Alliance == Alliance.Black
                ) ||
                (
                    BoardUtils.CoordinatesRow(Position) == BoardUtils.Row.Seventh && 
                    Alliance == Alliance.White
                )
            ) && 
            !board[destinationCoordinate].IsTileOccupied &&
            !board[behindDestinationCoordinate].IsTileOccupied;
    }

    protected override bool IsExclusion(int currentPosition, int transformation)
    {
        return BoardUtils.CoordinatesColumn(currentPosition) switch
        {
            BoardUtils.Column.First => transformation is 7 && Alliance == Alliance.Black || 
                                       transformation == 9 && Alliance == Alliance.White,
            BoardUtils.Column.Eighth => transformation is 7 && Alliance == Alliance.White || 
                                        transformation == 9 && Alliance == Alliance.Black,
            _ => false
        };
    }
}