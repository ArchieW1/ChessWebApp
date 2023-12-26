using ChessWebApp.ChessEngine.BoardLib;

namespace ChessWebApp.ChessEngine.MoveLib;

public partial class Move
{
    public static class Builder
    {
        private static readonly Move EmptyMove = new EmptyMove();

        public static Move CreateMove(Board board, int currentCoordinate, int destinationCoordinate)
        {
            foreach (Move move in board.AllLegalMoves)
            {
                if (move.DestinationCoordinate == destinationCoordinate &&
                    move.MovedPiece.Position == currentCoordinate)
                {
                    return move;
                }
            }
            return EmptyMove;
        }
    }
}