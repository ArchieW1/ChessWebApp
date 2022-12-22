using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.PlayerLib;

public sealed class BlackPlayer : Player
{
    public override Alliance Alliance { get; }
    public override IEnumerable<Piece> ActivePieces => _board.BlackPieces;
    public override Player Opponent => _board.WhitePlayer;
    
    public BlackPlayer(Board board, IEnumerable<Move> legalMoves, IEnumerable<Move> opponentsMoves) :
        base(board, legalMoves, opponentsMoves)
    {
        Alliance = Alliance.Black;
    }
    
    protected override bool CanShortCastle()
    {
        Tile rookTile = _board[7];
        return !_board[5].IsTileOccupied && !_board[6].IsTileOccupied &&
               rookTile.IsTileOccupied &&
               rookTile.Piece.IsFirstMove &&
               !CalculateAttacksOnTile(6, Opponent.LegalMoves).Any() &&
               rookTile.Piece is Rook;
    }

    protected override bool CanLongCastle()
    {
        Tile rookTile = _board[0];
        return !_board[1].IsTileOccupied && !_board[2].IsTileOccupied && !_board[3].IsTileOccupied &&
               rookTile.IsTileOccupied &&
               rookTile.Piece.IsFirstMove &&
               !CalculateAttacksOnTile(2, Opponent.LegalMoves).Any() &&
               rookTile.Piece is Rook;
    }
}