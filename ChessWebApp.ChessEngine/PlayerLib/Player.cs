using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.Pieces;

namespace ChessWebApp.ChessEngine.PlayerLib;

// TODO- continue
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

    // TODO- finish
    public bool IsInCheck => false;
    public bool IsInCheckmate => false;
    public bool IsInStalemate => false;
    
    private Board _board;
    private King _king;
    private readonly List<Move> _legalMoves;

    public Player(Board board, Alliance alliance, List<Move> legalMoves)
    {
        _board = board;
        _legalMoves = legalMoves;
        Alliance = alliance;
        _king = EstablishKing();
    }

    public bool IsLegalMove(Move move)
    {
        return _legalMoves.Contains(move);
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