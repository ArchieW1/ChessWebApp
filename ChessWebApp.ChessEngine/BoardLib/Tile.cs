using System.Collections.ObjectModel;
using ChessWebApp.ChessEngine.Pieces;

namespace ChessWebApp.ChessEngine.BoardLib;

public sealed class Tile
{
    public Piece? Piece { get; }
    public bool IsTileOccupied => Piece is not null;
    
    private int _tileCoordinate;
    
    private Tile(int tileCoordinate, Piece? piece = null)
    {
        _tileCoordinate = tileCoordinate;
        Piece = piece;
    }

    public static Tile CreateTile(int tileCoordinate, Piece? piece = null)
    {
        return piece is null ? EmptyTiles[tileCoordinate] : new Tile(tileCoordinate, piece);
    }
    
    private static readonly Dictionary<int, Tile> EmptyTiles = CreateAllPossibleEmptyTiles();
    
    private static Dictionary<int, Tile> CreateAllPossibleEmptyTiles()
    {
        Dictionary<int, Tile> emptyTileMap = new();
        
        for (int i = 0; i < Board.Utils.NumberOfTiles; i++)
        {
            emptyTileMap[i] = new Tile(i);
        }
        
        return new Dictionary<int, Tile>(emptyTileMap);
    }

    public override string ToString()
    {
        if (!IsTileOccupied)
        {
            return "-";
        }

        return Piece!.ToString();
    }
}