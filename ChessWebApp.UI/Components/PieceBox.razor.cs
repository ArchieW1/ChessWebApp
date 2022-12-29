using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ChessWebApp.UI.Components;

public sealed partial class PieceBox : ComponentBase
{
    [Parameter]
    public bool IsWhite { get; set; }

    private string DefaultColour => IsWhite ? "white" : "black";
    
    private string _colour = "";

    protected override Task OnInitializedAsync()
    {
        _colour = DefaultColour;
        return Task.CompletedTask;
    }
}