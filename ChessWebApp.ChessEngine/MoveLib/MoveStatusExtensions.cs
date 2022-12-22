namespace ChessWebApp.ChessEngine.MoveLib;

public static class MoveStatusExtensions
{
    public static bool IsDone(this MoveStatus status)
    {
        return status == MoveStatus.Done;
    }
}