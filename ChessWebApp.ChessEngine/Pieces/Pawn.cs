using System.Collections.ObjectModel;
using ChessWebApp.ChessEngine.Boardd;

namespace ChessWebApp.ChessEngine.Pieces;

public class Pawn : Piece
{
    private static readonly int[] CandidateMoveTransformations = {8, 16};
    
    public Pawn(int position, Alliance alliance) : base(position, alliance)
    {
    }

    public override ReadOnlyCollection<Move> CalculateLegalMoves(Board board)
    {
        List<Move> legalMoves = new List<Move>();

        foreach (int moveTransformation in CandidateMoveTransformations)
        {
            int destinationCoordinate = Position + moveTransformation * Alliance.Direction();

            if (!BoardUtils.IsValidCoordinate(destinationCoordinate))
            {
                continue;
            }

            if (moveTransformation == 8 && !Board.GetTile(destinationCoordinate).IsTileOccupied)
            {
                // TODO- Handle pawn move
                legalMoves.Add(new MajorMove(board, this, destinationCoordinate));
            }
        }
        
        return new ReadOnlyCollection<Move>(legalMoves);
    }

    protected override bool IsExclusion(int currentPosition, int transformation)
    {
        return false;
    }
}