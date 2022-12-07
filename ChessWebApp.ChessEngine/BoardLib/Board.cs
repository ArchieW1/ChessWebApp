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
        for (int i = 0; i < BoardUtils.NumberOfTiles; i++)
        {
            string tileText = _tiles[i].ToString();
            builder += tileText.PadRight(3);

            if ((i + 1) % BoardUtils.NumberOfColumns == 0)
            {
                builder += '\n';
            }
        }

        return builder;
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

    public static Board CreateStandardBoard() 
    {
        // abbreviate for repetitions sake
        static void Bset(Piece p) => Builder.SetPiece(p);
        const Alliance bl = Alliance.Black;
        const Alliance wh = Alliance.White;

        // black pieces
        Bset(new Rook(0, bl));
        Bset(new Knight(1, bl));
        Bset(new Bishop(2, bl));
        Bset(new Queen(3, bl));
        Bset(new King(4, bl));
        Bset(new Bishop(5, bl));
        Bset(new Knight(6, bl));
        Bset(new Rook(7, bl));
        Bset(new Pawn(8, bl));
        Bset(new Pawn(9, bl));
        Bset(new Pawn(10, bl));
        Bset(new Pawn(11, bl));
        Bset(new Pawn(12, bl));
        Bset(new Pawn(13, bl));
        Bset(new Pawn(14, bl));
        Bset(new Pawn(15, bl));

        // white pieces
        Bset(new Pawn(48, wh));
        Bset(new Pawn(49, wh));
        Bset(new Pawn(50, wh));
        Bset(new Pawn(51, wh));
        Bset(new Pawn(52, wh));
        Bset(new Pawn(53, wh));
        Bset(new Pawn(54, wh));
        Bset(new Pawn(55, wh));
        Bset(new Rook(56, wh));
        Bset(new Knight(57, wh));
        Bset(new Bishop(58, wh));
        Bset(new Queen(59, wh));
        Bset(new King(60, wh));
        Bset(new Bishop(61, wh));
        Bset(new Knight(62, wh));
        Bset(new Rook(63, wh));

        Builder.SetMoveMaker(wh);
        return Builder.Build();
    }
}