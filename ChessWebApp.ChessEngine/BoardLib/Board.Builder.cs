using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.BoardLib;

public sealed partial class Board
{
    public static class Builder
    {
        public static IDictionary<int, Piece> BoardConfig { get; } = new Dictionary<int, Piece>();
        public static Alliance NextMoveMaker { get; private set; } = Alliance.White;
        public static Pawn EnPassantPawn { get; private set; } = new(-1, Alliance.None);

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

        public static Board Build(Board board)
        {
            foreach (Tile tile in board)
            {
                BoardConfig[tile.TileCoordinate] = tile.Piece;
            }

            return new Board();
        }

        public static void SetEnPassantPawn(Pawn pawn)
        {
            EnPassantPawn = pawn;
        }
    }
}