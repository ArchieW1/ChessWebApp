using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.BoardLib;

public sealed class Tile
{
    public Piece Piece { get; }
    
    public bool IsTileOccupied { get; }
    
    public int TileCoordinate { get; }

    public Tile(int tileCoordinate, Piece piece)
    {
        TileCoordinate = tileCoordinate;
        Piece = piece;
        IsTileOccupied = piece is not Empty;
    }

    public override string ToString()
    {
        return Piece.ToString().PadRight(3);
    }
}