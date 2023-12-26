using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.PlayerLib;

public abstract class Player
{
    public abstract Alliance Alliance { get; }
    public abstract IEnumerable<Piece> ActivePieces { get; }
    public abstract Player Opponent { get; }

    public bool IsInCheck { get; }
    public bool IsInCheckmate => IsInCheck && !HasEscapeMoves();
    public bool IsInStalemate => !IsInCheck && !HasEscapeMoves();

    public King King { get; }
    public IEnumerable<Move> LegalMoves { get; }
    
    protected Board Board { get; }
    
    protected Player(Board board, IEnumerable<Move> legalMoves, IEnumerable<Move> opponentsMoves)
    {
        // avoiding multiple enumeration with seemingly redundant cast.
        List<Move> legalMovesList = legalMoves.ToList();
        List<Move> opponentsMovesList = opponentsMoves.ToList();
        
        Board = board;
        King = EstablishKing();
        legalMovesList.AddRange(CalculateKingCastles(opponentsMovesList));
        LegalMoves = legalMovesList;
        IsInCheck = CalculateAttacksOnTile(King.Position, opponentsMovesList).Any();
    }

    public bool IsLegalMove(Move move)
    {
        return LegalMoves.Contains(move);
    }
    
    protected static IEnumerable<Move> CalculateAttacksOnTile(int piecePosition, IEnumerable<Move> moves)
    {
        List<Move> attackMoves = new();
        foreach (Move move in moves)
        {
            if (piecePosition == move.DestinationCoordinate)
            {
                attackMoves.Add(move);
            }
        }
        return attackMoves;
    }

    protected bool HasEscapeMoves()
    {
        foreach (Move move in LegalMoves)
        {
            MoveTransition transition = MakeMove(move);
            if (transition.MoveStatus == MoveStatus.Done)
            {
                return true;
            }
        }
        return false;
    }

    public MoveTransition MakeMove(Move move)
    {
        if (move is EmptyMove || !IsLegalMove(move))
        {
            return new MoveTransition(Board, move, MoveStatus.Illegal);
        }

        Board transitionBoard = move.Execute();
        IEnumerable<Move> kingAttacks = CalculateAttacksOnTile(transitionBoard.CurrentPlayer.Opponent.King.Position,
            transitionBoard.CurrentPlayer.LegalMoves);
        move.UnExecute(move.MovedPiece);

        if (kingAttacks.Any())
        {
            return new MoveTransition(Board, move, MoveStatus.LeavesPlayerInCheck);
        }

        return new MoveTransition(transitionBoard, move, MoveStatus.Done);
    }

    private King EstablishKing()
    {
        foreach (Piece piece in ActivePieces)
        {
            if (piece is King king)
            {
                return king;
            }
        }
        throw new Exception("Not a valid chess board arrangement. There must be a king on the board.");
    }

    protected abstract IEnumerable<Move> CalculateKingCastles(IEnumerable<Move> opponentsLegals);

    protected abstract bool CanShortCastle(IEnumerable<Move> opponentsLegals);
    protected abstract bool CanLongCastle(IEnumerable<Move> opponentsLegals);
}