using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;

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

    public override Board UnExecute(Piece _)
    {
        throw new Exception("Not to be ran.");
    }
}