using ChessWebApp.ChessEngine.BoardLib;

namespace ChessWebApp.ChessEngine.AiLib;

public interface IBoardEvaluator
{
    public int Evaluate(Board board, int depth);
}