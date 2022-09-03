namespace ChessWebApp.UI.Shared.Models;

public class CoordinatePiece
{
    public CoordinatePiece(string coordinate, string? piece = null)
    {
        Coordinate = coordinate;
        Piece = piece;
    }

    public string Coordinate { get; set; }
    public string? Piece { get; set; }
}