using System.Collections.ObjectModel;
using ChessWebApp.ChessEngine.BoardLib;

namespace ChessWebApp.ChessEngine.Pieces;

public abstract class DirectionalPiece : Piece
{
    protected int[] CandidateMoveVectorTransformations { get; init; } = Array.Empty<int>();

    protected DirectionalPiece(int position, Alliance alliance) : base(position, alliance)
    {
    }

    public override ReadOnlyCollection<Move> CalculateLegalMoves(Board board)
    {
        List<Move> legalMoves = new List<Move>();

        foreach (int vectorTransformation in CandidateMoveVectorTransformations)
        {
            int destinationCoordinate = Position;
            while (BoardUtils.IsValidCoordinate(destinationCoordinate))
            {
                if (IsExclusion(destinationCoordinate, vectorTransformation))
                {
                    break;
                }
                
                destinationCoordinate += vectorTransformation;
                if (!BoardUtils.IsValidCoordinate(destinationCoordinate))
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

                break;
            }
        }

        return new ReadOnlyCollection<Move>(legalMoves);
    }
}