using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.BoardLib;

public sealed class Tile
{
    public Piece Piece { get; }
    
    public bool IsTileOccupied { get; }
    
    private int _tileCoordinate;

    public Tile(int tileCoordinate, Piece piece)
    {
        _tileCoordinate = tileCoordinate;
        Piece = piece;
        IsTileOccupied = piece is not Empty;
    }

    public override string ToString()
    {
        return Piece.ToString().PadRight(3);
    }
}