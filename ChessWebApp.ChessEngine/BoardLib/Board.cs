using ChessWebApp.ChessEngine.Pieces;

namespace ChessWebApp.ChessEngine.BoardLib;

public class Board
{
    private readonly List<Tile> _tiles;

    private Board()
    {
        _tiles = CreateGameBoard();
    }
    
    public Tile GetTile(int tileCoordinate)
    {
        return _tiles[tileCoordinate];
    }

    private static List<Tile> CreateGameBoard()
    {
        Tile[] tiles = new Tile[BoardUtils.NumberOfTiles];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = Tile.CreateTile(i, Builder.BoardConfig[i]);
        }
        return new List<Tile>(tiles);
    }
    
    public static class Builder
    {
        public static Dictionary<int, Piece> BoardConfig { get; } = new();
        private static Alliance _nextMoveMaker;

        public static void AddPiece(Piece piece)
        {
            BoardConfig.Add(piece.Position, piece);
        }

        public static void SetMoveMaker(Alliance alliance)
        {
            _nextMoveMaker = alliance;
        }

        public static Board Build()
        {
            return new Board();
        }
    }
}