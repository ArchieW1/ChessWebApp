namespace ChessWebApp.ChessEngine.PlayerLib;

public static class MoveStatusExtensions
{
    public static bool IsDone(this MoveStatus status)
    {
        return status == MoveStatus.Done;
    }
}