using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.Pieces;

namespace ChessWebApp.ChessEngine.PlayerLib;

public class Player
{
    public Alliance Alliance { get; }
    public List<Piece> ActivePieces =>
        Alliance switch
        {
            Alliance.White => _board.WhitePieces.ToList(),
            Alliance.Black => _board.BlackPieces.ToList(),
            _ => throw new IndexOutOfRangeException()
        };
    public bool IsInCheck { get; set; }
    public bool IsInCheckmate => IsInCheck && !HasEscapeMoves();
    public bool IsInStalemate => !IsInCheck && !HasEscapeMoves();

    public Player Opponent => Alliance switch
    {
        Alliance.White => _board.BlackPlayer,
        Alliance.Black => _board.WhitePlayer,
        _ => throw new IndexOutOfRangeException()
    };
    public King King { get; }
    public List<Move> LegalMoves { get; }
    
    private Board _board;
    

    public Player(Board board, Alliance alliance, IEnumerable<Move> legalMoves, IEnumerable<Move> opponentsMoves)
    {
        _board = board;
        LegalMoves = legalMoves.ToList();
        Alliance = alliance;
        King = EstablishKing();
        IsInCheck = CalculateAttacksOnTile(King.Position, opponentsMoves).Any();
    }

    public bool IsLegalMove(Move move)
    {
        return LegalMoves.Contains(move);
    }
    
    private static IEnumerable<Move> CalculateAttacksOnTile(int piecePosition, IEnumerable<Move> moves)
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

    private bool HasEscapeMoves()
    {
        foreach (Move move in LegalMoves)
        {
            MoveTransition transition = MakeMove(move);
            if (transition.MoveStatus.IsDone())
            {
                return true;
            }
        }
        return false;
    }

    private MoveTransition MakeMove(Move move)
    {
        if (!IsLegalMove(move))
        {
            return new MoveTransition(_board, move, MoveStatus.Illegal);
        }

        Board transitionBoard = move.Execute();
        IEnumerable<Move> kingAttacks = CalculateAttacksOnTile(transitionBoard.CurrentPlayer.Opponent.King.Position,
            transitionBoard.CurrentPlayer.LegalMoves);

        if (kingAttacks.Any())
        {
            return new MoveTransition(_board, move, MoveStatus.LeavesPlayerInCheck);
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
}