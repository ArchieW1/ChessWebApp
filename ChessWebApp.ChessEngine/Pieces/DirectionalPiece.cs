using System.Collections.ObjectModel;
using ChessWebApp.ChessEngine.BoardLib;

namespace ChessWebApp.ChessEngine.Pieces;

public abstract class DirectionalPiece : Piece
{
    protected int[] CandidateMoveVectorTransformations { get; init; } = Array.Empty<int>();

    protected DirectionalPiece(int position, Alliance alliance) : base(position, alliance)
    {
    }

    public override IEnumerable<Move> CalculateLegalMoves(Board board)
    {
        List<Move> legalMoves = new List<Move>();

        foreach (int vectorTransformation in CandidateMoveVectorTransformations)
        {
            int destinationCoordinate = Position;
            while (destinationCoordinate.IsValid())
            {
                if (IsExclusion(destinationCoordinate, vectorTransformation))
                {
                    break;
                }
                
                destinationCoordinate += vectorTransformation;
                if (!destinationCoordinate.IsValid())
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

                break;
            }
        }

        return new ReadOnlyCollection<Move>(legalMoves);
    }
}