using ChessWebApp.ChessEngine.BoardLib;

namespace ChessWebApp.ChessEngine.MoveLib;

public sealed class MoveTransition
{
    public MoveStatus MoveStatus { get; }
    public Move Move { get; }
    public Board Board { get; }

    public MoveTransition(Board transitionBoard, Move move, MoveStatus moveStatus)
    {
        MoveStatus = moveStatus;
        Move = move;
        Board = transitionBoard;
    }
    
}