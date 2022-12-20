using ChessWebApp.ChessEngine.Pieces;

namespace ChessWebApp.ChessEngine.BoardLib;

public sealed partial class Board
{
    public static class Builder
    {
        public static Dictionary<int, Piece> BoardConfig { get; } = new();
        public static Alliance NextMoveMaker { get; set; }

        public static void SetPiece(Piece piece)
        {
            BoardConfig[piece.Position] = piece;
        }

        public static void SetMoveMaker(Alliance alliance)
        {
            NextMoveMaker = alliance;
        }

        public static Board Build()
        {
            return new Board();
        }
        
        public static Board CreateStandardBoard() 
        {
            // abbreviate for repetitions sake
            const Alliance bl = Alliance.Black;
            const Alliance wh = Alliance.White;
    
            // black pieces
            SetPiece(new Rook(0, bl));
            SetPiece(new Knight(1, bl));
            SetPiece(new Bishop(2, bl));
            SetPiece(new Queen(3, bl));
            SetPiece(new King(4, bl));
            SetPiece(new Bishop(5, bl));
            SetPiece(new Knight(6, bl));
            SetPiece(new Rook(7, bl));
            SetPiece(new Pawn(8, bl));
            SetPiece(new Pawn(9, bl));
            SetPiece(new Pawn(10, bl));
            SetPiece(new Pawn(11, bl));
            SetPiece(new Pawn(12, bl));
            SetPiece(new Pawn(13, bl));
            SetPiece(new Pawn(14, bl));
            SetPiece(new Pawn(15, bl));

            // empty squares
            for (int i = 16; i <= 47; i++)
            {
                SetPiece(new Empty(i));
            }
    
            // white pieces
            SetPiece(new Pawn(48, wh));
            SetPiece(new Pawn(49, wh));
            SetPiece(new Pawn(50, wh));
            SetPiece(new Pawn(51, wh));
            SetPiece(new Pawn(52, wh));
            SetPiece(new Pawn(53, wh));
            SetPiece(new Pawn(54, wh));
            SetPiece(new Pawn(55, wh));
            SetPiece(new Rook(56, wh));
            SetPiece(new Knight(57, wh));
            SetPiece(new Bishop(58, wh));
            SetPiece(new Queen(59, wh));
            SetPiece(new King(60, wh));
            SetPiece(new Bishop(61, wh));
            SetPiece(new Knight(62, wh));
            SetPiece(new Rook(63, wh));
    
            SetMoveMaker(wh);
            return Build();
        }
    }
}