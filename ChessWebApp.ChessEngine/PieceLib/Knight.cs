using System.Collections.ObjectModel;
using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;

namespace ChessWebApp.ChessEngine.PieceLib;

public sealed class Knight : Piece
{
    private static readonly int[] CandidateMoveTransformations = {-17, -15, -10, -6, 6, 10, 15, 17};
    
    public Knight(int position, Alliance alliance, bool isFirstMove = true) : base(position, alliance, isFirstMove)
    {
        Symbol = "N";
        PieceValue = 300;
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
            Board.Utils.Row.First => transformation is -17 or -10 or 6 or 15,
            Board.Utils.Row.Second => transformation is -10 or 6,
            Board.Utils.Row.Seventh => transformation is -6 or 10,
            Board.Utils.Row.Eighth => transformation is -15 or -6 or 10 or 17,
            _ => false
        };
    }
    
    public override Knight MovePiece(Move move)
    {
        return new Knight(move.DestinationCoordinate, move.MovedPiece.Alliance, false);
    }
}