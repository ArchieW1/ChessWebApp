namespace ChessWebApp.UI.Models;

public class CoordinatePiece
{
    public string Coordinate { get; set; }
    public string? Piece { get; set; }

    public CoordinatePiece(string coordinate, string? piece = null)
    {
        Coordinate = coordinate;
        Piece = piece;
    }
}