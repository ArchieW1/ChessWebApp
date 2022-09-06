using ChessWebApp.UI.Models;

namespace ChessWebApp.UI.Services;

public class BoardService : IBoardService
{
    public CoordinatePiece[] PieceLocations { get; set; } =
    {
        new("a8", "black-rook"),
        new("b8", "black-knight"),
        new("c8", "black-bishop"),
        new("d8", "black-queen"),
        new("e8", "black-king"),
        new("f8", "black-bishop"),
        new("g8", "black-knight"),
        new("h8", "black-rook"),
        new("a7", "black-pawn"),
        new("b7", "black-pawn"),
        new("c7", "black-pawn"),
        new("d7", "black-pawn"),
        new("e7", "black-pawn"),
        new("f7", "black-pawn"),
        new("g7", "black-pawn"),
        new("h7", "black-pawn"),
    
        new("a6"),
        new("b6"),
        new("c6"),
        new("d6"),
        new("e6"),
        new("f6"),
        new("g6"),
        new("h6"),
        
        new("a5"),
        new("b5"),
        new("c5"),
        new("d5"),
        new("e5"),
        new("f5"),
        new("g5"),
        new("h5"),
        
        new("a4"),
        new("b4"),
        new("c4"),
        new("d4"),
        new("e4"),
        new("f4"),
        new("g4"),
        new("h4"),
        
        new("a3"),
        new("b3"),
        new("c3"),
        new("d3"),
        new("e3"),
        new("f3"),
        new("g3"),
        new("h3"),

        new("a2", "white-pawn"),
        new("b2", "white-pawn"),
        new("c2", "white-pawn"),
        new("d2", "white-pawn"),
        new("e2", "white-pawn"),
        new("f2", "white-pawn"),
        new("g2", "white-pawn"),
        new("h2", "white-pawn"),
        new("a1", "white-rook"),
        new("b1", "white-knight"),
        new("c1", "white-bishop"),
        new("d1", "white-queen"),
        new("e1", "white-king"),
        new("f1", "white-bishop"),
        new("g1", "white-knight"),
        new("h1", "white-rook")
    };

    public int SelectedIndex { get; set; }
    public CoordinatePiece? SelectedPiece { get; set; }
    
    public int FindIndexOfCoordinate(string coordinate)
    {
        CoordinatePiece localPiece = PieceLocations.First(p => p.Coordinate == coordinate);
        return Array.IndexOf(PieceLocations, localPiece);
    }

    public void SetSelectedByCoordinate(string coordinate)
    {
        SelectedIndex = FindIndexOfCoordinate(coordinate);
        SelectedPiece = PieceLocations[SelectedIndex];
    }

    public void SetSelectedToEmpty()
    {
        PieceLocations[SelectedIndex].Piece = null;
        SelectedPiece = null;
    }

    public void ChangeCoordinateToSelected(string coordinate)
    {
        PieceLocations[FindIndexOfCoordinate(coordinate)].Piece = 
            SelectedPiece!.Piece;
    }
}