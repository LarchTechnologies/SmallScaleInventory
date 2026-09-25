using System.Globalization;

namespace InventoryManagement.Tests;

/// <summary>Runs a block with a fixed culture so number formats in messages are predictable.</summary>
internal sealed class TestCulture : IDisposable
{
    private readonly CultureInfo _previous;
    private readonly CultureInfo _previousUi;

    public TestCulture(string name = "en-US")
    {
        _previous = CultureInfo.CurrentCulture;
        _previousUi = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentCulture = new CultureInfo(name);
        CultureInfo.CurrentUICulture = new CultureInfo(name);
    }

    public void Dispose()
    {
        CultureInfo.CurrentCulture = _previous;
        CultureInfo.CurrentUICulture = _previousUi;
    }
}
