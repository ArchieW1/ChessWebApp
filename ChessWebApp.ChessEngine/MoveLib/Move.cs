using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.MoveLib;

public partial class Move
{
    public int DestinationCoordinate { get; }
    public Piece MovedPiece { get; }
    public bool IsFirstMove { get; }
    public Board Board { get; }
    
    public Move(Board board, Piece movedPiece, int destinationCoordinate)
    {
        Board = board;
        MovedPiece = movedPiece;
        DestinationCoordinate = destinationCoordinate;
        IsFirstMove = movedPiece.IsFirstMove;
    }
    
    protected Move(int destinationCoordinate)
    {
        Board = Board.Builder.Build();
        MovedPiece = new Empty(-1);
        DestinationCoordinate = destinationCoordinate;
        IsFirstMove = false;
    }

    public virtual Board Execute()
    {
        KeepStateExcludingPieces(MovedPiece);

        Board.Builder.SetPiece(new Empty(MovedPiece.Position));
        Board.Builder.SetPiece(MovedPiece.MovePiece(this));
        Board.Builder.SetMoveMaker(Board.CurrentPlayer.Opponent.Alliance);
        
        if (MovedPiece is Pawn)
        {
            Board.Builder.SetEnPassantPawn(new Pawn(-1, Alliance.None));
        }
        
        return Board.Builder.Build();
    }

    public virtual Board UnExecute(Piece oldPiece)
    {
        KeepStateExcludingPieces(MovedPiece);
        
        Board.Builder.SetPiece(oldPiece);
        Board.Builder.SetPiece(MovedPiece);
        Board.Builder.SetMoveMaker(Board.CurrentPlayer.Alliance);

        return Board.Builder.Build();
    }

    public void KeepStateExcludingPieces(params Piece[] excludePieces)
    {
        foreach (Piece piece in Board.CurrentPlayer.ActivePieces)
        {
            bool isExcluded = false;
            foreach (Piece excludePiece in excludePieces)
            {
                if (MovedPiece == excludePiece)
                {
                    isExcluded = true;
                    break;
                }
            }

            if (!isExcluded)
            {
                Board.Builder.SetPiece(piece);
            }
        }

        foreach (Piece piece in Board.CurrentPlayer.Opponent.ActivePieces)
        {
            Board.Builder.SetPiece(piece);
        }
    }
}