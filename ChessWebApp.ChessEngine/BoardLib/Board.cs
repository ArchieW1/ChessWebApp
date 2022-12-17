using ChessWebApp.ChessEngine.Pieces;

namespace ChessWebApp.ChessEngine.BoardLib;

public sealed partial class Board
{
    private readonly List<Tile> _tiles;
    private readonly IEnumerable<Piece> _whitePieces;
    private readonly IEnumerable<Piece> _blackPieces;

    private Board()
    {
        _tiles = CreateGameBoard();
        _whitePieces = CalculateActivePieces(_tiles, Alliance.White);
        _blackPieces = CalculateActivePieces(_tiles, Alliance.Black);

        IEnumerable<Move> whiteStandardLegalMoves = CalculateLegalMoves(_whitePieces);
        IEnumerable<Move> blackStandardLegalMoves = CalculateLegalMoves(_blackPieces);
    }

    public Tile this[int index] => _tiles[index];

    public override string ToString()
    {
        string builder = string.Empty;
        for (int i = 0; i < Utils.NumberOfTiles; i++)
        {
            string tileText = _tiles[i].ToString();
            builder += tileText.PadRight(3);

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
            tiles[i] = Tile.CreateTile(i, Builder.BoardConfig.GetValueOrDefault(i));
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

            Piece piece = tile.Piece!;
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