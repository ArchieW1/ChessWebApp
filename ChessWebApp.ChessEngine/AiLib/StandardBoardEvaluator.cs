using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PieceLib;
using ChessWebApp.ChessEngine.PlayerLib;

namespace ChessWebApp.ChessEngine.AiLib;

public class StandardBoardEvaluator : IBoardEvaluator
{
    private const int CheckmateBonus = 10000;
    private const int CheckBonus = 50;
    private const int DepthBonus = 100;
    
    public int Evaluate(Board board, int depth)
    {
        return ScorePlayer(board, board.WhitePlayer, depth) -
               ScorePlayer(board, board.BlackPlayer, depth);
    }

    private int ScorePlayer(Board board, Player player, int depth)
    {
        return PieceValue(player) +
               Mobility(player) +
               Check(player, depth) +
               Checkmate(player, depth);
    }

    private int Checkmate(Player player, int depth)
    {
        return player.Opponent.IsInCheckmate ? CheckmateBonus * Depth(depth) : 0;
    }

    private static int Depth(int depth)
    {
        return depth == 0 ? 1 : DepthBonus * depth;
    }

    private int Check(Player player, int depth)
    {
        return player.Opponent.IsInCheck ? CheckBonus * Depth(depth) : 0;
    }

    private int Mobility(Player player)
    {
        return player.LegalMoves.Count();
    }

    private int PieceValue(Player player)
    {
        int pieceValueScore = 0;
        foreach (Piece piece in player.ActivePieces)
        {
            pieceValueScore += piece.PieceValue;
        }

        return pieceValueScore;
    }
}