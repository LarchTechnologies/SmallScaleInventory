namespace InventoryManagement.Helpers;

/// <summary>Shows the wait cursor for the lifetime of a using block.</summary>
public sealed class WaitCursorScope : IDisposable
{
    private readonly Cursor _previous;

    public WaitCursorScope()
    {
        _previous = Cursor.Current ?? Cursors.Default;
        Cursor.Current = Cursors.WaitCursor;
    }

    public void Dispose()
    {
        Cursor.Current = _previous;
    }
}
