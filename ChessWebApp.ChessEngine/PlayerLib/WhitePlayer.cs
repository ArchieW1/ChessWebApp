using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;
using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.PlayerLib;

public sealed class WhitePlayer : Player
{
    public override Alliance Alliance { get; }
    public override IEnumerable<Piece> ActivePieces => Board.WhitePieces;
    public override Player Opponent => Board.BlackPlayer;
    
    public WhitePlayer(Board board, IEnumerable<Move> legalMoves, IEnumerable<Move> opponentsMoves) :
        base(board, legalMoves, opponentsMoves)
    {
        Alliance = Alliance.White;
    }

    protected override IEnumerable<Move> CalculateKingCastles(IEnumerable<Move> opponentsLegals)
    {
        List<Move> kingCastles = new();

        List<Move> opponentsLegalsList = opponentsLegals.ToList();
        
        if (CanShortCastle(opponentsLegalsList))
        {
            Tile rookTile = Board[63];
            if (rookTile.Piece is not Rook rook)
            {
                throw new ApplicationException("If can short castle the piece in the rook tile should be a rook");
            }
            
            kingCastles.Add(new Castle(Board, King, 62, rook, rookTile.TileCoordinate, 61));
        }

        if (CanLongCastle(opponentsLegalsList))
        {
            Tile rookTile = Board[56];
            if (rookTile.Piece is not Rook rook)
            {
                throw new ApplicationException("If can short castle the piece in the rook tile should be a rook");
            }
            
            kingCastles.Add(new Castle(Board, King, 58, rook, rookTile.TileCoordinate, 59));
        }

        return kingCastles;
    }

    protected override bool CanShortCastle(IEnumerable<Move> opponentsLegals)
    {
        Tile rookTile = Board[63];
        Tile kingTile = Board[60];
        return !Board[61].IsTileOccupied && !Board[62].IsTileOccupied &&
               rookTile.IsTileOccupied && 
               kingTile.IsTileOccupied &&
               rookTile.Piece.IsFirstMove &&
               kingTile.Piece.IsFirstMove &&
               !CalculateAttacksOnTile(62, opponentsLegals).Any() &&
               rookTile.Piece is Rook &&
               kingTile.Piece is King;
    }

    protected override bool CanLongCastle(IEnumerable<Move> opponentsLegals)
    {
        Tile rookTile = Board[56];
        Tile kingTile = Board[60];
        return !Board[59].IsTileOccupied && !Board[58].IsTileOccupied && !Board[57].IsTileOccupied &&
               rookTile.IsTileOccupied &&
               kingTile.IsTileOccupied &&
               rookTile.Piece.IsFirstMove &&
               kingTile.Piece.IsFirstMove &&
               !CalculateAttacksOnTile(58, opponentsLegals).Any() &&
               rookTile.Piece is Rook &&
               kingTile.Piece is King;
    }
}