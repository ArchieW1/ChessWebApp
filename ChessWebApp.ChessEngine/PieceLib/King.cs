using System.Collections.ObjectModel;
using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;

namespace ChessWebApp.ChessEngine.PieceLib;

public sealed class King : Piece
{
    private static readonly int[] CandidateMoveTransformations = {-9, -8, -7, -1, 1, 7, 8, 9};

    public King(int position, Alliance alliance, bool isFirstMove = true) 
        : base(position, alliance, isFirstMove)
    {
        Symbol = "K";
        IsFirstMove = isFirstMove;
        PieceValue = 10000;
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
                legalMoves.Add(new Move(board, this, destinationCoordinate));
                continue;
            }
            
            Piece pieceAtDestination = destinationCoordinateTile.Piece;
            if (Alliance != pieceAtDestination.Alliance)
            {
                legalMoves.Add(new Move(board, this, destinationCoordinate));
            }
        }

        return new ReadOnlyCollection<Move>(legalMoves);
    }

    protected override bool IsExclusion(int currentPosition, int transformation)
    {
        return currentPosition.ToRow() switch
        {
            Board.Utils.Row.First => transformation is -9 or -1 or 7,
            Board.Utils.Row.Eighth => transformation is -7 or 1 or 9,
            _ => false
        };
    }
    
    public override King MovePiece(Move move)
    {
        return new King(move.DestinationCoordinate, move.MovedPiece.Alliance, false);
    }
}