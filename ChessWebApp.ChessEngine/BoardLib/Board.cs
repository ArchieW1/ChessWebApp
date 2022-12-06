using ChessWebApp.ChessEngine.Pieces;

namespace ChessWebApp.ChessEngine.BoardLib;

public class Board
{
    private readonly List<Tile> _tiles;
    private readonly IEnumerable<Piece> _whitePieces;
    private readonly IEnumerable<Piece> _blackPieces;

    private Board()
    {
        _tiles = CreateGameBoard();
        _whitePieces = CalculateActivePieces(_tiles, Alliance.White);
        _blackPieces = CalculateActivePieces(_tiles, Alliance.Black);
    }

    public Tile this[int index]
    {
        get => _tiles[index];
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
            tiles[i] = Tile.CreateTile(i, Builder.BoardConfig.GetValueOrDefault(i));
        }
        return new List<Tile>(tiles);
    }

    private static IEnumerable<Piece> CalculateActivePieces(List<Tile> tiles, Alliance alliance)
    {
        List<Piece> activePieces = new();

        foreach (Tile tile in tiles)
        {
            if (!tile.IsTileOccupied)
            {
                continue;
            }

            Piece piece = tile.Piece!;
            if (piece.Alliance == alliance)
            {
                activePieces.Add(piece);
            }
        }

        return activePieces;
    }

    public static Board CreateStandardBoard() 
    {
        // abbreviate for repetitions sake
        static void bset(Piece p) => Builder.SetPiece(p);
        Alliance bl = Alliance.Black;
        Alliance wh = Alliance.White;

        // black pices
        bset(new Rook(0, bl));
        bset(new Knight(1, bl));
        bset(new Bishop(2, bl));
        bset(new Queen(3, bl));
        bset(new King(4, bl));
        bset(new Bishop(5, bl));
        bset(new Knight(6, bl));
        bset(new Rook(7, bl));
        bset(new Pawn(8, bl));
        bset(new Pawn(9, bl));
        bset(new Pawn(10, bl));
        bset(new Pawn(11, bl));
        bset(new Pawn(12, bl));
        bset(new Pawn(13, bl));
        bset(new Pawn(14, bl));
        bset(new Pawn(15, bl));

        // white pieces
        bset(new Pawn(48, wh));
        bset(new Pawn(49, wh));
        bset(new Pawn(50, wh));
        bset(new Pawn(51, wh));
        bset(new Pawn(52, wh));
        bset(new Pawn(53, wh));
        bset(new Pawn(54, wh));
        bset(new Pawn(55, wh));
        bset(new Rook(56, wh));
        bset(new Knight(57, wh));
        bset(new Bishop(58, wh));
        bset(new Queen(59, wh));
        bset(new King(60, wh));
        bset(new Bishop(61, wh));
        bset(new Knight(62, wh));
        bset(new Rook(63, wh));

        Builder.SetMoveMaker(wh);
        return Builder.Build();
    }
    
    public static class Builder
    {
        public static Dictionary<int, Piece> BoardConfig { get; } = new();
        private static Alliance _nextMoveMaker;

        public static void SetPiece(Piece piece)
        {
            BoardConfig[piece.Position] = piece;
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