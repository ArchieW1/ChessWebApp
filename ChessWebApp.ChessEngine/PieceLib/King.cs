using System.Collections.ObjectModel;
using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;

namespace ChessWebApp.ChessEngine.PieceLib;

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

            if (!destinationCoordinate.IsValid() || IsExclusion(Position, moveTransformation))
            {
                continue;
            }

            Tile destinationCoordinateTile = board[destinationCoordinate];
            if (!destinationCoordinateTile.IsTileOccupied)
            {
                legalMoves.Add(new StandardMove(board, this, destinationCoordinate));
                continue;
            }
            
            Piece pieceAtDestination = destinationCoordinateTile.Piece;
            if (Alliance != pieceAtDestination.Alliance)
            {
                legalMoves.Add(new Attack(board, this, destinationCoordinate, pieceAtDestination));
            }
        }

        return new ReadOnlyCollection<Move>(legalMoves);
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
        return new King(move.DestinationCoordinate, move.MovedPiece.Alliance);
    }
}