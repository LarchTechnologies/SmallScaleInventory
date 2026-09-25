using System.ComponentModel;
using InventoryManagement.Helpers;

namespace InventoryManagement.Controls;

/// <summary>Dashboard statistic card: title, big value, subtitle and a coloured accent bar.</summary>
public partial class StatCard : UserControl
{
    private const int AccentWidth = 5;
    private Color _accentColor = Color.FromArgb(37, 99, 235);

    public StatCard()
    {
        InitializeComponent();
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);

        BackColor = UiTheme.CardBack;
        lblTitle.ForeColor = UiTheme.TextMuted;
        lblValue.ForeColor = UiTheme.TextPrimary;
        lblSubtitle.ForeColor = UiTheme.TextMuted;
    }

    [Category("Appearance")]
    [Description("Caption shown at the top of the card.")]
    [DefaultValue("Title")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string Title
    {
        get => lblTitle.Text;
        set => lblTitle.Text = value;
    }

    [Category("Appearance")]
    [Description("Main figure shown in large text.")]
    [DefaultValue("0")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string Value
    {
        get => lblValue.Text;
        set => lblValue.Text = value;
    }

    [Category("Appearance")]
    [Description("Small explanatory text under the value.")]
    [DefaultValue("")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string Subtitle
    {
        get => lblSubtitle.Text;
        set => lblSubtitle.Text = value;
    }

    [Category("Appearance")]
    [Description("Colour of the bar on the left side of the card.")]
    [DefaultValue(typeof(Color), "37, 99, 235")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color AccentColor
    {
        get => _accentColor;
        set
        {
            _accentColor = value;
            Invalidate();
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        using (var accent = new SolidBrush(_accentColor))
        {
            e.Graphics.FillRectangle(accent, 0, 0, AccentWidth, Height);
        }

        using var border = new Pen(UiTheme.Border);
        e.Graphics.DrawRectangle(border, 0, 0, Width - 1, Height - 1);
    }
}
