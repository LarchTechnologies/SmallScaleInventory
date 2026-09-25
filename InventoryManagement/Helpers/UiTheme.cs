using InventoryManagement.Models;

namespace InventoryManagement.Helpers;

/// <summary>
/// Central colours, fonts and control styles so every screen looks the same.
/// Forms call these methods from their constructor after InitializeComponent().
/// </summary>
public static class UiTheme
{
    // ---- Palette -----------------------------------------------------------
    public static readonly Color SidebarBack = Color.FromArgb(15, 23, 42);
    public static readonly Color SidebarHover = Color.FromArgb(30, 41, 59);
    public static readonly Color SidebarText = Color.FromArgb(203, 213, 225);
    public static readonly Color SidebarSectionText = Color.FromArgb(100, 116, 139);

    public static readonly Color ContentBack = Color.FromArgb(241, 245, 249);
    public static readonly Color CardBack = Color.White;
    public static readonly Color Border = Color.FromArgb(226, 232, 240);

    public static readonly Color TextPrimary = Color.FromArgb(15, 23, 42);
    public static readonly Color TextMuted = Color.FromArgb(100, 116, 139);

    public static readonly Color Primary = Color.FromArgb(37, 99, 235);
    public static readonly Color PrimaryHover = Color.FromArgb(29, 78, 216);
    public static readonly Color Success = Color.FromArgb(22, 163, 74);
    public static readonly Color SuccessHover = Color.FromArgb(21, 128, 61);
    public static readonly Color Danger = Color.FromArgb(220, 38, 38);
    public static readonly Color DangerHover = Color.FromArgb(185, 28, 28);
    public static readonly Color Warning = Color.FromArgb(217, 119, 6);
    public static readonly Color Violet = Color.FromArgb(124, 58, 237);

    public static readonly Color StatusNormalBack = Color.FromArgb(220, 252, 231);
    public static readonly Color StatusNormalText = Color.FromArgb(22, 101, 52);
    public static readonly Color StatusLowBack = Color.FromArgb(254, 243, 199);
    public static readonly Color StatusLowText = Color.FromArgb(146, 64, 14);
    public static readonly Color StatusOutBack = Color.FromArgb(254, 226, 226);
    public static readonly Color StatusOutText = Color.FromArgb(153, 27, 27);

    public static readonly Color GridHeaderBack = Color.FromArgb(248, 250, 252);
    public static readonly Color GridAlternateBack = Color.FromArgb(248, 250, 252);
    public static readonly Color GridSelectionBack = Color.FromArgb(219, 234, 254);
    public static readonly Color InactiveText = Color.FromArgb(148, 163, 184);

    // ---- Fonts ---------------------------------------------------------------
    public const string FontFamilyName = "Segoe UI";

    public static readonly Font BaseFont = new(FontFamilyName, 9.75F, FontStyle.Regular);
    public static readonly Font BoldFont = new(FontFamilyName, 9.75F, FontStyle.Bold);
    public static readonly Font SectionTitleFont = new(FontFamilyName, 11.25F, FontStyle.Bold);
    public static readonly Font PageTitleFont = new(FontFamilyName, 15.75F, FontStyle.Bold);
    public static readonly Font SmallFont = new(FontFamilyName, 8.25F, FontStyle.Regular);
    public static readonly Font BadgeFont = new(FontFamilyName, 8.25F, FontStyle.Bold);
    public static readonly Font LargeValueFont = new(FontFamilyName, 18F, FontStyle.Bold);

    /// <summary>Background and base font for a content page.</summary>
    public static void ApplyPage(Form form)
    {
        form.BackColor = ContentBack;
        form.ForeColor = TextPrimary;
        form.Font = BaseFont;
    }

    /// <summary>White "card" panel with a thin border.</summary>
    public static void StyleCard(Control panel)
    {
        panel.BackColor = CardBack;
        panel.Paint -= PaintCardBorder;
        panel.Paint += PaintCardBorder;
    }

    public static void StyleSectionTitle(Label label)
    {
        label.Font = SectionTitleFont;
        label.ForeColor = TextPrimary;
    }

    public static void StyleMutedLabel(Label label)
    {
        label.ForeColor = TextMuted;
    }

    public static void StylePrimaryButton(Button button) => StyleFilledButton(button, Primary, PrimaryHover);

    public static void StyleSuccessButton(Button button) => StyleFilledButton(button, Success, SuccessHover);

    public static void StyleDangerButton(Button button) => StyleFilledButton(button, Danger, DangerHover);

    /// <summary>White button with a border - for secondary actions (Clear, Print ...).</summary>
    public static void StyleSecondaryButton(Button button)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.BackColor = CardBack;
        button.ForeColor = TextPrimary;
        button.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
        button.FlatAppearance.BorderSize = 1;
        button.FlatAppearance.MouseOverBackColor = Color.FromArgb(241, 245, 249);
        button.FlatAppearance.MouseDownBackColor = Border;
        button.Cursor = Cursors.Hand;
        button.Font = BaseFont;
        button.UseVisualStyleBackColor = false;
    }

    /// <summary>Consistent look and behaviour for every data grid.</summary>
    public static void StyleGrid(DataGridView grid)
    {
        grid.AutoGenerateColumns = false;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.ReadOnly = true;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.RowHeadersVisible = false;
        grid.BackgroundColor = CardBack;
        grid.BorderStyle = BorderStyle.None;
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        grid.GridColor = Border;
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        grid.ColumnHeadersHeight = 38;
        grid.RowTemplate.Height = 32;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.Font = BaseFont;

        grid.ColumnHeadersDefaultCellStyle.BackColor = GridHeaderBack;
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);
        grid.ColumnHeadersDefaultCellStyle.Font = BoldFont;
        grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = GridHeaderBack;
        grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 0, 6, 0);

        grid.DefaultCellStyle.BackColor = CardBack;
        grid.DefaultCellStyle.ForeColor = TextPrimary;
        grid.DefaultCellStyle.SelectionBackColor = GridSelectionBack;
        grid.DefaultCellStyle.SelectionForeColor = TextPrimary;
        grid.DefaultCellStyle.Padding = new Padding(6, 0, 6, 0);

        grid.AlternatingRowsDefaultCellStyle.BackColor = GridAlternateBack;
    }

    /// <summary>Background / text colours for a stock status value.</summary>
    public static (Color Back, Color Fore) GetStatusColors(string? stockStatus) => stockStatus switch
    {
        StockStatus.OutOfStock => (StatusOutBack, StatusOutText),
        StockStatus.Low => (StatusLowBack, StatusLowText),
        _ => (StatusNormalBack, StatusNormalText),
    };

    /// <summary>Makes a label look like a coloured status "badge" (used for legends).</summary>
    public static void StyleStatusBadge(Label label, string stockStatus)
    {
        (Color back, Color fore) = GetStatusColors(stockStatus);
        label.BackColor = back;
        label.ForeColor = fore;
        label.Font = BadgeFont;
        label.Text = stockStatus;
        label.TextAlign = ContentAlignment.MiddleCenter;
        label.AutoSize = false;
        label.Size = new Size(TextRenderer.MeasureText(stockStatus, label.Font).Width + 18, 22);
    }

    private static void StyleFilledButton(Button button, Color back, Color hover)
    {
        button.FlatStyle = FlatStyle.Flat;
        button.BackColor = back;
        button.ForeColor = Color.White;
        button.FlatAppearance.BorderSize = 0;
        button.FlatAppearance.MouseOverBackColor = hover;
        button.FlatAppearance.MouseDownBackColor = hover;
        button.Cursor = Cursors.Hand;
        button.Font = BoldFont;
        button.UseVisualStyleBackColor = false;
    }

    private static void PaintCardBorder(object? sender, PaintEventArgs e)
    {
        if (sender is Control control)
        {
            using var pen = new Pen(Border);
            e.Graphics.DrawRectangle(pen, 0, 0, control.Width - 1, control.Height - 1);
        }
    }
}
