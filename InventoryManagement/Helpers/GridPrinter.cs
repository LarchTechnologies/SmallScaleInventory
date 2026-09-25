using System.Drawing.Printing;

namespace InventoryManagement.Helpers;

/// <summary>
/// Prints the visible columns of any DataGridView as a simple, paginated,
/// landscape report (title, filter description, header row, data rows,
/// totals line and page numbers). Shown through the standard Print Preview
/// dialog, from which the user can print or save to PDF ("Microsoft Print to PDF").
/// Used by all three reports, so new reports get printing for free.
/// </summary>
public sealed class GridPrinter : IDisposable
{
    private const int CellPadding = 4;

    private readonly DataGridView _grid;
    private readonly string _title;
    private readonly IReadOnlyList<string> _subtitleLines;
    private readonly string? _footerLine;
    private readonly PrintDocument _document = new();
    private readonly List<DataGridViewColumn> _columns;

    private readonly Font _titleFont = new(UiTheme.FontFamilyName, 14F, FontStyle.Bold);
    private readonly Font _subtitleFont = new(UiTheme.FontFamilyName, 9F);
    private readonly Font _headerFont = new(UiTheme.FontFamilyName, 8.5F, FontStyle.Bold);
    private readonly Font _cellFont = new(UiTheme.FontFamilyName, 8.5F);
    private readonly Font _footerFont = new(UiTheme.FontFamilyName, 8F);

    private int _nextRowIndex;
    private int _pageNumber;
    private DateTime _printedAt;

    private GridPrinter(DataGridView grid, string title, IEnumerable<string> subtitleLines, string? footerLine)
    {
        _grid = grid;
        _title = title;
        _subtitleLines = subtitleLines.Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
        _footerLine = footerLine;
        _columns = grid.Columns.Cast<DataGridViewColumn>()
            .Where(c => c.Visible)
            .OrderBy(c => c.DisplayIndex)
            .ToList();

        _document.DocumentName = title;
        _document.DefaultPageSettings.Landscape = true;
        _document.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);
        _document.BeginPrint += (_, _) =>
        {
            _nextRowIndex = 0;
            _pageNumber = 0;
            _printedAt = DateTime.Now;
        };
        _document.PrintPage += PrintPage;
    }

    /// <summary>Opens the print preview for the grid.</summary>
    public static void ShowPreview(IWin32Window owner, DataGridView grid, string title, IEnumerable<string> subtitleLines, string? footerLine = null)
    {
        if (grid.Rows.Count == 0)
        {
            MessageHelper.ShowInfo("There is nothing to print. Load the report first.");
            return;
        }

        try
        {
            using var printer = new GridPrinter(grid, title, subtitleLines, footerLine);
            using var preview = new PrintPreviewDialog
            {
                Document = printer._document,
                Width = 1100,
                Height = 800,
                StartPosition = FormStartPosition.CenterParent,
                UseAntiAlias = true,
                ShowIcon = false,
                Text = "Print Preview - " + title,
            };
            preview.ShowDialog(owner);
        }
        catch (InvalidPrinterException)
        {
            MessageHelper.ShowWarning("No printer is available. Install a printer (for example 'Microsoft Print to PDF') to print reports.");
        }
    }

    public void Dispose()
    {
        _document.Dispose();
        _titleFont.Dispose();
        _subtitleFont.Dispose();
        _headerFont.Dispose();
        _cellFont.Dispose();
        _footerFont.Dispose();
    }

    private void PrintPage(object? sender, PrintPageEventArgs e)
    {
        Graphics g = e.Graphics!;
        Rectangle bounds = e.MarginBounds;
        _pageNumber++;

        float y = bounds.Top;
        y = DrawPageHeader(g, bounds, y);

        float[] widths = CalculateColumnWidths(bounds.Width);
        float rowHeight = _cellFont.GetHeight(g) + (CellPadding * 2);
        float footerSpace = (_footerFont.GetHeight(g) * 3) + 10;

        y = DrawHeaderRow(g, bounds, widths, y, rowHeight);

        using var linePen = new Pen(Color.FromArgb(220, 220, 220));
        while (_nextRowIndex < _grid.Rows.Count)
        {
            if (y + rowHeight > bounds.Bottom - footerSpace)
            {
                e.HasMorePages = true;
                DrawPageFooter(g, bounds);
                return;
            }

            DrawDataRow(g, bounds, widths, _grid.Rows[_nextRowIndex], y, rowHeight);
            y += rowHeight;
            g.DrawLine(linePen, bounds.Left, y, bounds.Right, y);
            _nextRowIndex++;
        }

        if (!string.IsNullOrWhiteSpace(_footerLine))
        {
            g.DrawString(_footerLine, _headerFont, Brushes.Black, bounds.Left, y + 8);
        }

        e.HasMorePages = false;
        DrawPageFooter(g, bounds);
    }

    private float DrawPageHeader(Graphics g, Rectangle bounds, float y)
    {
        if (_pageNumber == 1)
        {
            g.DrawString(_title, _titleFont, Brushes.Black, bounds.Left, y);
            y += _titleFont.GetHeight(g) + 4;

            foreach (string line in _subtitleLines)
            {
                g.DrawString(line, _subtitleFont, Brushes.DimGray, bounds.Left, y);
                y += _subtitleFont.GetHeight(g) + 1;
            }

            g.DrawString($"Printed: {_printedAt.ToString(UiFormats.DateTime)}", _subtitleFont, Brushes.DimGray, bounds.Left, y);
            y += _subtitleFont.GetHeight(g) + 10;
        }
        else
        {
            g.DrawString(_title + " (continued)", _subtitleFont, Brushes.DimGray, bounds.Left, y);
            y += _subtitleFont.GetHeight(g) + 8;
        }

        return y;
    }

    private float DrawHeaderRow(Graphics g, Rectangle bounds, float[] widths, float y, float rowHeight)
    {
        using var headerBrush = new SolidBrush(Color.FromArgb(235, 239, 245));
        g.FillRectangle(headerBrush, bounds.Left, y, bounds.Width, rowHeight);

        float x = bounds.Left;
        for (int i = 0; i < _columns.Count; i++)
        {
            DrawCellText(g, _columns[i].HeaderText, _headerFont, _columns[i].DefaultCellStyle.Alignment, x, y, widths[i], rowHeight);
            x += widths[i];
        }

        return y + rowHeight;
    }

    private void DrawDataRow(Graphics g, Rectangle bounds, float[] widths, DataGridViewRow row, float y, float rowHeight)
    {
        float x = bounds.Left;
        for (int i = 0; i < _columns.Count; i++)
        {
            DataGridViewCell cell = row.Cells[_columns[i].Index];
            string text = cell.FormattedValue?.ToString() ?? string.Empty;
            DrawCellText(g, text, _cellFont, _columns[i].DefaultCellStyle.Alignment, x, y, widths[i], rowHeight);
            x += widths[i];
        }
    }

    private void DrawPageFooter(Graphics g, Rectangle bounds)
    {
        float y = bounds.Bottom - _footerFont.GetHeight(g);
        g.DrawString(MessageHelper.ApplicationTitle, _footerFont, Brushes.Gray, bounds.Left, y);

        string page = $"Page {_pageNumber}";
        SizeF size = g.MeasureString(page, _footerFont);
        g.DrawString(page, _footerFont, Brushes.Gray, bounds.Right - size.Width, y);
    }

    private float[] CalculateColumnWidths(int availableWidth)
    {
        float total = _columns.Sum(c => (float)Math.Max(c.Width, 40));
        return _columns.Select(c => Math.Max(c.Width, 40) / total * availableWidth).ToArray();
    }

    private static void DrawCellText(Graphics g, string text, Font font, DataGridViewContentAlignment alignment, float x, float y, float width, float height)
    {
        using var format = new StringFormat
        {
            Trimming = StringTrimming.EllipsisCharacter,
            FormatFlags = StringFormatFlags.NoWrap,
            LineAlignment = StringAlignment.Center,
            Alignment = alignment switch
            {
                DataGridViewContentAlignment.MiddleRight or DataGridViewContentAlignment.TopRight or DataGridViewContentAlignment.BottomRight => StringAlignment.Far,
                DataGridViewContentAlignment.MiddleCenter or DataGridViewContentAlignment.TopCenter or DataGridViewContentAlignment.BottomCenter => StringAlignment.Center,
                _ => StringAlignment.Near,
            },
        };

        var rectangle = new RectangleF(x + CellPadding, y, width - (CellPadding * 2), height);
        g.DrawString(text, font, Brushes.Black, rectangle, format);
    }
}
