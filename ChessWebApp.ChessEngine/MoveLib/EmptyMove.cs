using ChessWebApp.ChessEngine.BoardLib;

namespace ChessWebApp.ChessEngine.MoveLib;

public sealed class EmptyMove : Move
{
    public EmptyMove() : base(default)
    {
    }

    public override Board Execute()
    {
        throw new Exception("Not to be ran.");
    }
}