using ChessWebApp.ChessEngine.PieceLib;

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
        
    }
}