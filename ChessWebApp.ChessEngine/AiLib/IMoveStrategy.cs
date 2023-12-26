using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;

namespace ChessWebApp.ChessEngine.AiLib;

public interface IMoveStrategy
{
    public Move Execute(Board board);
}