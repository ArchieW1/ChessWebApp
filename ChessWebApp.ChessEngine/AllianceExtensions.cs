using ChessWebApp.ChessEngine.BoardLib;
using ChessWebApp.ChessEngine.PlayerLib;

namespace ChessWebApp.ChessEngine;

public static class AllianceExtensions
{
    public static int GetDirection(this Alliance alliance)
    {
        return alliance switch
        {
            Alliance.White => -1,
            Alliance.Black => 1,
            _ => 0
        };
    }

    public static Player ChoosePlayer(this Alliance alliance, Player whitePlayer, Player blackPlayer)
    {
        return alliance switch
        {
            Alliance.White => whitePlayer,
            Alliance.Black => blackPlayer,
            _ => throw new IndexOutOfRangeException()
        };
    }
    
    public static bool IsPromotionSquare(this Alliance alliance, int position)
    {
        return alliance switch
        {
            Alliance.White => position.ToColumn() == Board.Utils.Column.First,
            Alliance.Black => position.ToColumn() == Board.Utils.Column.Eighth,
            _ => throw new ArgumentOutOfRangeException(nameof(alliance), alliance, null)
        };
    }
}