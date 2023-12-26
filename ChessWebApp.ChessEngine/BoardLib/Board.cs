using System.Collections;
using ChessWebApp.ChessEngine.MoveLib;
using ChessWebApp.ChessEngine.PieceLib;
using ChessWebApp.ChessEngine.PlayerLib;

namespace ChessWebApp.ChessEngine.BoardLib;

public sealed partial class Board : IEnumerable<Tile>
{
    public IEnumerable<Piece> WhitePieces { get; }
    public IEnumerable<Piece> BlackPieces { get; }
    public Player CurrentPlayer { get; }
    public Player WhitePlayer { get; }
    public Player BlackPlayer { get; }
    public Pawn EnPassantPawn { get; }
    public IEnumerable<Move> AllLegalMoves 
    {
        get
        {
            List<Move> allLegalMoves = new();
            allLegalMoves.AddRange(WhitePlayer.LegalMoves);
            allLegalMoves.AddRange(BlackPlayer.LegalMoves);
            return allLegalMoves;
        }
    }

    private readonly List<Tile> _tiles;
    
    private Board()
    {
        EnPassantPawn = Builder.EnPassantPawn;
        
        _tiles = CreateGameBoard();
        WhitePieces = CalculateActivePieces(_tiles, Alliance.White);
        BlackPieces = CalculateActivePieces(_tiles, Alliance.Black);

        List<Move> whiteStandardLegalMoves = CalculateLegalMoves(WhitePieces).ToList();
        List<Move> blackStandardLegalMoves = CalculateLegalMoves(BlackPieces).ToList();
        WhitePlayer = new WhitePlayer(this, whiteStandardLegalMoves, blackStandardLegalMoves);
        BlackPlayer = new BlackPlayer(this, blackStandardLegalMoves, whiteStandardLegalMoves);

        CurrentPlayer = Builder.NextMoveMaker.ChoosePlayer(WhitePlayer, BlackPlayer);
    }

    public Tile this[int index] => _tiles[index];

    public IEnumerator<Tile> GetEnumerator()
    {
        return _tiles.GetEnumerator();
    }

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

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static Board CreateStandardBoard() 
    {
        // abbreviate for repetitions sake
        static void SetP(Piece piece) => Builder.SetPiece(piece);
        const Alliance bl = Alliance.Black;
        const Alliance wh = Alliance.White;

        // black pieces
        SetP(new Rook(0, bl));
        SetP(new Knight(1, bl));
        SetP(new Bishop(2, bl));
        SetP(new Queen(3, bl));
        SetP(new King(4, bl));
        SetP(new Bishop(5, bl));
        SetP(new Knight(6, bl));
        SetP(new Rook(7, bl));
        SetP(new Pawn(8, bl));
        SetP(new Pawn(9, bl));
        SetP(new Pawn(10, bl));
        SetP(new Pawn(11, bl));
        SetP(new Pawn(12, bl));
        SetP(new Pawn(13, bl));
        SetP(new Pawn(14, bl));
        SetP(new Pawn(15, bl));

        // empty squares
        for (int i = 16; i <= 47; i++)
        {
            SetP(new Empty(i));
        }

        // white pieces
        SetP(new Pawn(48, wh));
        SetP(new Pawn(49, wh));
        SetP(new Pawn(50, wh));
        SetP(new Pawn(51, wh));
        SetP(new Pawn(52, wh));
        SetP(new Pawn(53, wh));
        SetP(new Pawn(54, wh));
        SetP(new Pawn(55, wh));
        SetP(new Rook(56, wh));
        SetP(new Knight(57, wh));
        SetP(new Bishop(58, wh));
        SetP(new Queen(59, wh));
        SetP(new King(60, wh));
        SetP(new Bishop(61, wh));
        SetP(new Knight(62, wh));
        SetP(new Rook(63, wh));

        Builder.SetMoveMaker(wh);
        return Builder.Build();
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