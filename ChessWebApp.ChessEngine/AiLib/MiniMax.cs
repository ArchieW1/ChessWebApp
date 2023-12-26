using System.Diagnostics;
using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.MoveLib;

namespace ChessWebApp.ChessEngine.AiLib;

public class MiniMax : IMoveStrategy
{
    private static Board _savedBoardState = Board.Builder.Build();
    private readonly IBoardEvaluator _boardEvaluator;
    private readonly int _depth;

    public MiniMax(IBoardEvaluator boardEvaluator, int depth)
    {
        _boardEvaluator = boardEvaluator;
        _depth = depth;
    }
    
    public Move Execute(Board board)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        
        Move bestMove = new EmptyMove();
        _savedBoardState = Board.Builder.Build(board);
        
        int highestSeenValue = int.MinValue;
        int lowestSeenValue = int.MaxValue;

        Console.WriteLine($"Bot thinking with depth {_depth}.");

        foreach (Move move in board.CurrentPlayer.LegalMoves)
        {
            MoveTransition transition = board.CurrentPlayer.MakeMove(move);

            int currentValue = board.CurrentPlayer.Alliance == Alliance.White
                ? Min(transition.Board, _depth - 1)
                : Max(transition.Board, _depth - 1);
            
            switch (board.CurrentPlayer.Alliance)
            {
                case Alliance.White when currentValue >= highestSeenValue:
                    highestSeenValue = currentValue;
                    bestMove = move;
                    break;
                case Alliance.Black when currentValue <= lowestSeenValue:
                    lowestSeenValue = currentValue;
                    bestMove = move;
                    break;
            }
        }
        
        stopwatch.Stop();
        Console.WriteLine($"Best move found in {stopwatch.ElapsedMilliseconds}ms.");
        board = Board.Builder.Build(_savedBoardState);
        return bestMove;
    }
    
    private static bool IsEndGameScenario(Board board)
    {
        return board.CurrentPlayer.IsInCheckmate || board.CurrentPlayer.IsInStalemate;
    }
    
    public int Min(Board board, int depth)
    {
        if (depth == 0 || IsEndGameScenario(board))
        {
            return _boardEvaluator.Evaluate(board, depth);
        }
        
        int lowestSeenValue = int.MaxValue;
        foreach (Move move in board.CurrentPlayer.LegalMoves)
        {
            MoveTransition transition = board.CurrentPlayer.MakeMove(move);

            int currentValue = Max(transition.Board, depth - 1);
            
            if (currentValue <= lowestSeenValue)
            {
                lowestSeenValue = currentValue;
            }
        }
        
        return lowestSeenValue;
    }
    
    public int Max(Board board, int depth)
    {
        if (depth == 0 || IsEndGameScenario(board))
        {
            return _boardEvaluator.Evaluate(board, depth);
        }
        
        int highestSeenValue = int.MinValue;
        foreach (Move move in board.CurrentPlayer.LegalMoves)
        {
            MoveTransition transition = board.CurrentPlayer.MakeMove(move);

            int currentValue = Min(transition.Board, depth - 1);

            if (currentValue >= highestSeenValue)
            {
                highestSeenValue = currentValue;
            }
        }
        
        return highestSeenValue;
    }
}