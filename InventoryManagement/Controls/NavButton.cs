using System.ComponentModel;
using InventoryManagement.Helpers;

namespace InventoryManagement.Controls;

/// <summary>Sidebar navigation button with an "active page" highlight bar.</summary>
public class NavButton : Button
{
    private const int AccentWidth = 4;
    private bool _isActive;

    public NavButton()
    {
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;
        FlatAppearance.MouseOverBackColor = UiTheme.SidebarHover;
        FlatAppearance.MouseDownBackColor = UiTheme.SidebarHover;
        BackColor = UiTheme.SidebarBack;
        ForeColor = UiTheme.SidebarText;
        TextAlign = ContentAlignment.MiddleLeft;
        Padding = new Padding(18, 0, 0, 0);
        Cursor = Cursors.Hand;
        UseVisualStyleBackColor = false;
        TabStop = false;
    }

    /// <summary>The page this button opens (set by MainForm).</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Forms.AppPage Page { get; set; }

    /// <summary>True when this button's page is currently displayed.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            BackColor = value ? UiTheme.SidebarHover : UiTheme.SidebarBack;
            ForeColor = value ? Color.White : UiTheme.SidebarText;
            Font = value ? UiTheme.BoldFont : UiTheme.BaseFont;
            Invalidate();
        }
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);

        if (_isActive)
        {
            using var brush = new SolidBrush(UiTheme.Primary);
            pevent.Graphics.FillRectangle(brush, 0, 0, AccentWidth, Height);
        }
    }
}
