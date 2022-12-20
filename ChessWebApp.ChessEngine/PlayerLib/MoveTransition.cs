using ChessWebApp.ChessEngine.BoardLib;

namespace ChessWebApp.ChessEngine.PlayerLib;

public sealed class MoveTransition
{
    public MoveStatus MoveStatus { get; set; }
    public Move Move { get; set; }
    public Board Board { get; set; }

    public MoveTransition(Board transitionBoard, Move move, MoveStatus moveStatus)
    {
        MoveStatus = moveStatus;
        Move = move;
        Board = transitionBoard;
    }
    
}