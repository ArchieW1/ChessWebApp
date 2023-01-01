using System.Collections.ObjectModel;
using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;

namespace ChessWebApp.ChessEngine.PieceLib;

public sealed class Pawn : Piece
{
    private static readonly int[] CandidateMoveTransformations = {7, 8, 9, 16};

    public Pawn(int position, Alliance alliance, bool isFirstMove = true) : base(position, alliance, false)
    {
        Symbol = "P";
    }
    
    public override IEnumerable<Move> CalculateLegalMoves(Board board)
    {
        List<Move> legalMoves = new();

        foreach (int moveTransformation in CandidateMoveTransformations)
        {
            int destinationCoordinate = Position + moveTransformation * Alliance.GetDirection();

            if (!destinationCoordinate.IsValid() || IsExclusion(Position, moveTransformation))
            {
                continue;
            }

            Tile destinationTile = board[destinationCoordinate];

            if (moveTransformation is 8 && !destinationTile.IsTileOccupied)
            {
                legalMoves.Add(new PawnMove(board, this, destinationCoordinate));
                continue;
            }

            if (IsJumpMove(board, moveTransformation, destinationCoordinate))
            {
                legalMoves.Add(new PawnJump(board, this, destinationCoordinate));
                continue;
            }

            if (moveTransformation is 7 or 9 && destinationTile.IsTileOccupied && 
                destinationTile.Piece.Alliance != Alliance)
            {
                legalMoves.Add(new PawnAttack(board, this, destinationCoordinate, destinationTile.Piece));
                continue;
            }
            
        }
        
        return new ReadOnlyCollection<Move>(legalMoves);
    }

    private bool IsJumpMove(Board board, int moveTransformation, int destinationCoordinate)
    {
        int behindDestinationCoordinate = Position + Alliance.GetDirection() * 8;
        return 
            moveTransformation == 16 &&
            IsFirstMove &&
            (
                (
                    Position.ToColumn() == Board.Utils.Column.Second &&
                    Alliance == Alliance.Black
                ) ||
                (
                    Position.ToColumn() == Board.Utils.Column.Seventh && 
                    Alliance == Alliance.White
                )
            ) && 
            !board[destinationCoordinate].IsTileOccupied &&
            !board[behindDestinationCoordinate].IsTileOccupied;
    }

    protected override bool IsExclusion(int currentPosition, int transformation)
    {
        return currentPosition.ToColumn() switch
        {
            Board.Utils.Column.First => transformation is 7 && Alliance == Alliance.Black || 
                                       transformation == 9 && Alliance == Alliance.White,
            Board.Utils.Column.Eighth => transformation is 7 && Alliance == Alliance.White || 
                                        transformation == 9 && Alliance == Alliance.Black,
            _ => false
        };
    }
    
    public override Pawn MovePiece(Move move)
    {
        return new Pawn(move.DestinationCoordinate, move.MovedPiece.Alliance);
    }
}