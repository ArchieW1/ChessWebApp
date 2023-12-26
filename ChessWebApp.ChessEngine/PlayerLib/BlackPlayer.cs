using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.PlayerLib;

public sealed class BlackPlayer : Player
{
    public override Alliance Alliance { get; }
    public override IEnumerable<Piece> ActivePieces => Board.BlackPieces;
    public override Player Opponent => Board.WhitePlayer;
    
    public BlackPlayer(Board board, IEnumerable<Move> legalMoves, IEnumerable<Move> opponentsMoves) :
        base(board, legalMoves, opponentsMoves)
    {
        Alliance = Alliance.Black;
    }
    
    protected override IEnumerable<Move> CalculateKingCastles(IEnumerable<Move> opponentsLegals)
    {
        List<Move> kingCastles = new();

        List<Move> opponentsLegalMovesList = opponentsLegals.ToList();
        
        if (CanShortCastle(opponentsLegalMovesList))
        {
            Tile rookTile = Board[7];
            if (rookTile.Piece is not Rook rook)
            {
                throw new ApplicationException("If can short castle the piece in the rook tile should be a rook");
            }
            
            kingCastles.Add(new Castle(Board, King, 6, rook, rookTile.TileCoordinate, 5));
        }

        if (CanLongCastle(opponentsLegalMovesList))
        {
            Tile rookTile = Board[0];
            if (rookTile.Piece is not Rook rook)
            {
                throw new ApplicationException("If can short castle the piece in the rook tile should be a rook");
            }
            
            kingCastles.Add(new Castle(Board, King, 2, rook, rookTile.TileCoordinate, 3));
        }

        return kingCastles;
    }

    protected override bool CanShortCastle(IEnumerable<Move> opponentsLegals)
    {
        Tile rookTile = Board[7];
        Tile kingTile = Board[4];
        return !Board[5].IsTileOccupied && !Board[6].IsTileOccupied &&
               rookTile.IsTileOccupied &&
               kingTile.IsTileOccupied &&
               rookTile.Piece.IsFirstMove &&
               kingTile.Piece.IsFirstMove &&
               !CalculateAttacksOnTile(6, opponentsLegals).Any() &&
               rookTile.Piece is Rook &&
               kingTile.Piece is King;
    }

    protected override bool CanLongCastle(IEnumerable<Move> opponentsLegals)
    {
        Tile rookTile = Board[0];
        Tile kingTile = Board[4];
        return !Board[1].IsTileOccupied && !Board[2].IsTileOccupied && !Board[3].IsTileOccupied &&
               rookTile.IsTileOccupied &&
               kingTile.IsTileOccupied &&
               rookTile.Piece.IsFirstMove &&
               kingTile.Piece.IsFirstMove &&
               !CalculateAttacksOnTile(2, opponentsLegals).Any() &&
               rookTile.Piece is Rook &&
               kingTile.Piece is King;
    }
}