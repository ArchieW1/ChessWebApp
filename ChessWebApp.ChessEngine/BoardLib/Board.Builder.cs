using ChessWebApp.ChessEngine.Pieces;

namespace ChessWebApp.ChessEngine.BoardLib;

public sealed partial class Board
{
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