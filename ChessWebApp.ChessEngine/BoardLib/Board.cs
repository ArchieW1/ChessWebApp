using ChessWebApp.ChessEngine.Pieces;

namespace ChessWebApp.ChessEngine.BoardLib;

public sealed partial class Board
{
    public IEnumerable<Piece> WhitePieces { get; }
    public IEnumerable<Piece> BlackPieces { get; }
    
    private readonly List<Tile> _tiles;
    
    private Board()
    {
        _tiles = CreateGameBoard();
        WhitePieces = CalculateActivePieces(_tiles, Alliance.White);
        BlackPieces = CalculateActivePieces(_tiles, Alliance.Black);

        IEnumerable<Move> whiteStandardLegalMoves = CalculateLegalMoves(WhitePieces);
        IEnumerable<Move> blackStandardLegalMoves = CalculateLegalMoves(BlackPieces);
    }

    public Tile this[int index] => _tiles[index];

    public override string ToString()
    {
        string builder = string.Empty;
        for (int i = 0; i < Utils.NumberOfTiles; i++)
        {
            builder += _tiles[i].ToString();

            if ((i + 1) % Utils.NumberOfColumns == 0)
            {
                builder += '\n';
            }
        }

        return builder;
    }

    private static List<Tile> CreateGameBoard()
    {
        Tile[] tiles = new Tile[Utils.NumberOfTiles];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new Tile(i, Builder.BoardConfig[i]);
        }
        return new List<Tile>(tiles);
    }

    private static IEnumerable<Piece> CalculateActivePieces(IEnumerable<Tile> tiles, Alliance alliance)
    {
        List<Piece> activePieces = new();

        foreach (Tile tile in tiles)
        {
            if (!tile.IsTileOccupied)
            {
                continue;
            }

            Piece piece = tile.Piece;
            if (piece.Alliance == alliance)
            {
                activePieces.Add(piece);
            }
        }

        return activePieces;
    }

    private IEnumerable<Move> CalculateLegalMoves(IEnumerable<Piece> pieces)
    {
        List<Move> legalMoves = new();
        foreach (Piece piece in pieces)
        {
            legalMoves.AddRange(piece.CalculateLegalMoves(this));
        }
        return legalMoves;
    }
}