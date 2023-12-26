using ValueOf;

namespace ChessWebApp.Api.Domain.Common;

public sealed class Salt : ValueOf<string, Salt>
{
    protected override void Validate()
    {
    }
}