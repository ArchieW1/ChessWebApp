using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.PlayerLib;

public sealed class WhitePlayer : Player
{
    public override Alliance Alliance { get; }
    public override IEnumerable<Piece> ActivePieces => _board.WhitePieces;
    public override Player Opponent => _board.BlackPlayer;
    
    public WhitePlayer(Board board, IEnumerable<Move> legalMoves, IEnumerable<Move> opponentsMoves) :
        base(board, legalMoves, opponentsMoves)
    {
        Alliance = Alliance.White;
    }
    
    protected override bool CanShortCastle()
    {
        Tile rookTile = _board[63];
        return !_board[61].IsTileOccupied && !_board[62].IsTileOccupied &&
               rookTile.IsTileOccupied &&
               rookTile.Piece.IsFirstMove &&
               !CalculateAttacksOnTile(62, Opponent.LegalMoves).Any() &&
               rookTile.Piece is Rook;
    }

    protected override bool CanLongCastle()
    {
        Tile rookTile = _board[56];
        return !_board[59].IsTileOccupied && !_board[58].IsTileOccupied && !_board[57].IsTileOccupied &&
               rookTile.IsTileOccupied &&
               rookTile.Piece.IsFirstMove &&
               !CalculateAttacksOnTile(58, Opponent.LegalMoves).Any() &&
               rookTile.Piece is Rook;
    }
}