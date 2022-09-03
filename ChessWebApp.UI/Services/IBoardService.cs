using ChessWebApp.UI.Shared.Models;

namespace ChessWebApp.UI.Services;

public interface IBoardService
{
    public CoordinatePiece[] PieceLocations { get; set; }
    public int SelectedIndex { get; set; }
    public CoordinatePiece? SelectedPiece { get; set; }

    public int FindIndexOfCoordinate(string coordinate);

    public void SetSelectedByCoordinate(string coordinate);

    public void SetSelectedToEmpty();

    public void ChangeCoordinateToSelected(string coordinate);
}