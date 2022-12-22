using ChessWebApp.ChessEngine.PieceLib;

namespace ChessWebApp.ChessEngine.BoardLib;

public sealed partial class Board
{
    public static class Builder
    {
        public static IDictionary<int, Piece> BoardConfig { get; } = new Dictionary<int, Piece>();
        public static Alliance NextMoveMaker { get; private set; }

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

        public static void SetEnPassantPawn(Pawn pawn)
        {
            
        }
    }
}