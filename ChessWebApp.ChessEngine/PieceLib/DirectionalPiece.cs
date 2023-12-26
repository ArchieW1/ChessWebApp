using System.Collections.ObjectModel;
using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;

namespace ChessWebApp.ChessEngine.PieceLib;

public abstract class DirectionalPiece : Piece
{
    protected int[] CandidateMoveVectorTransformations { get; init; } = Array.Empty<int>();

    protected DirectionalPiece(int position, Alliance alliance, bool isFirstMove) : base(position, alliance, 
        isFirstMove)
    {
    }

    public override IEnumerable<Move> CalculateLegalMoves(Board board)
    {
        List<Move> legalMoves = new();

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

                Piece pieceAtDestination = destinationCoordinateTile.Piece;
                if (Alliance != pieceAtDestination.Alliance)
                {
                    legalMoves.Add(new Move(board, this, destinationCoordinate));
                }

                break;
            }
        }

        return new ReadOnlyCollection<Move>(legalMoves);
    }
}