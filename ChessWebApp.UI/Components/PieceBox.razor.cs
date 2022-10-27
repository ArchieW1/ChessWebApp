using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ChessWebApp.UI.Components;

public sealed partial class PieceBox : ComponentBase
{
    [Parameter]
    public string? Piece { get; set; }
    
    [Parameter]
    public string? Coordinate { get; set; }

    [Parameter]
    public bool IsWhite { get; set; }
    
    [Parameter]
    public EventCallback<string> OnUpdate { get; set; }

    private string DefaultColour => IsWhite ? "white" : "black";
    
    private string _colour = "";

    protected override Task OnInitializedAsync()
    {
        _colour = DefaultColour;
        return Task.CompletedTask;
    }

    private void MouseEnterHandler(MouseEventArgs e)
    {
        _colour = "selected";
    }

    private void MouseLeaveHandler(MouseEventArgs e)
    {
        _colour = DefaultColour;
    }

    private void ClickHandler(MouseEventArgs e)
    {
        if (Coordinate is null)
        {
            return;
        }

        if (_boardService.SelectedPiece is null)
        {
            _boardService.SetSelectedByCoordinate(Coordinate);
        }
        else
        {
            _boardService.ChangeCoordinateToSelected(Coordinate);
            _boardService.SetSelectedToEmpty();
            OnUpdate.InvokeAsync(null);
        }
    }
}