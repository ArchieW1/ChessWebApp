using System.Collections.ObjectModel;
using ChessWebApp.ChessEngine.Pieces;

namespace ChessWebApp.ChessEngine.Boardd;

public record Tile
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
    
    private static readonly ReadOnlyDictionary<int, Tile> EmptyTiles = CreateAllPossibleEmptyTiles();
    
    private static ReadOnlyDictionary<int, Tile> CreateAllPossibleEmptyTiles()
    {
        Dictionary<int, Tile> emptyTileMap = new();
        
        const int numOfTilesInBoard = 64;
        for (int i = 0; i < numOfTilesInBoard; i++)
        {
            emptyTileMap.Add(1, new Tile(i));
        }
        
        return new ReadOnlyDictionary<int, Tile>(emptyTileMap);
    }
}