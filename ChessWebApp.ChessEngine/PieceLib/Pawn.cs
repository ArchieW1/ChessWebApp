using System.Collections.ObjectModel;
using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;

namespace ChessWebApp.ChessEngine.PieceLib;

public sealed class Pawn : Piece
{
    private static readonly int[] CandidateMoveTransformations = {7, 8, 9, 16};

    public Pawn(int position, Alliance alliance, bool isFirstMove = true) : base(position, alliance, isFirstMove)
    {
        Symbol = "P";
        PieceValue = 100;
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
                if (Alliance.IsPromotionSquare(destinationCoordinate))
                {
                    legalMoves.Add(new Promotion(new Move(board, this, destinationCoordinate)));
                    continue;
                }
                
                legalMoves.Add(new Move(board, this, destinationCoordinate));
                continue;
            }

            if (IsJumpMove(board, moveTransformation, destinationCoordinate))
            {
                legalMoves.Add(new PawnJump(board, this, destinationCoordinate));
                continue;
            }

            switch (moveTransformation)
            {
                case 7 or 9 when destinationTile.IsTileOccupied && destinationTile.Piece.Alliance != Alliance:
                {
                    if (Alliance.IsPromotionSquare(destinationCoordinate))
                    {
                        legalMoves.Add(new Promotion(new Move(board, this, destinationCoordinate)));
                        continue;
                    }

                    legalMoves.Add(new Move(board, this, destinationCoordinate));
                    continue;
                }
                case 7 or 9 when board.EnPassantPawn is not null &&
                                 board.EnPassantPawn.Position + Alliance.GetDirection() * Board.Utils.NumberOfRows ==
                                 destinationCoordinate:
                {
                    if (Alliance.IsPromotionSquare(destinationCoordinate))
                    {
                        legalMoves.Add(new EnPassant(board, this, destinationCoordinate));
                        continue;
                    }
                    
                    legalMoves.Add(new EnPassant(board, this, destinationCoordinate));
                    continue;
                }
            }
        }
        
        return new ReadOnlyCollection<Move>(legalMoves);
    }

    private bool IsJumpMove(Board board, int moveTransformation, int destinationCoordinate)
    {
        int behindDestinationCoordinate = Position + Alliance.GetDirection() * Board.Utils.NumberOfRows;
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
        return currentPosition.ToRow() switch
        {
            Board.Utils.Row.First => transformation is 7 && Alliance == Alliance.Black || 
                                       transformation is 9 && Alliance == Alliance.White,
            Board.Utils.Row.Eighth => transformation is 7 && Alliance == Alliance.White || 
                                        transformation is 9 && Alliance == Alliance.Black,
            _ => false
        };
    }
    
    public override Pawn MovePiece(Move move)
    {
        return new Pawn(move.DestinationCoordinate, move.MovedPiece.Alliance, false);
    }
}