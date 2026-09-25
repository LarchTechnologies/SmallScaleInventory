namespace InventoryManagement.Helpers;

/// <summary>Quick period choices for report date filters.</summary>
public static class DateRangePresets
{
    public const string Custom = "Custom";
    public const string Today = "Today";
    public const string Last7Days = "Last 7 days";
    public const string ThisMonth = "This month";
    public const string LastMonth = "Last month";
    public const string ThisYear = "This year";

    public static IReadOnlyList<string> All { get; } = new[] { Custom, Today, Last7Days, ThisMonth, LastMonth, ThisYear };

    /// <summary>Returns the From/To dates for a preset, or null for Custom.</summary>
    public static (DateTime From, DateTime To)? GetRange(string? preset, DateTime today)
    {
        DateTime firstOfMonth = new(today.Year, today.Month, 1);
        return preset switch
        {
            Today => (today, today),
            Last7Days => (today.AddDays(-6), today),
            ThisMonth => (firstOfMonth, today),
            LastMonth => (firstOfMonth.AddMonths(-1), firstOfMonth.AddDays(-1)),
            ThisYear => (new DateTime(today.Year, 1, 1), today),
            _ => null,
        };
    }

    /// <summary>Fills the period combo and keeps the two date pickers in sync with it.</summary>
    public static void Attach(ComboBox periodCombo, DateTimePicker fromPicker, DateTimePicker toPicker, string initialPreset)
    {
        periodCombo.DropDownStyle = ComboBoxStyle.DropDownList;
        periodCombo.Items.Clear();
        periodCombo.Items.AddRange(All.Cast<object>().ToArray());

        bool applying = false;

        periodCombo.SelectedIndexChanged += (_, _) =>
        {
            (DateTime From, DateTime To)? range = GetRange(periodCombo.SelectedItem as string, DateTime.Today);
            if (range is null)
            {
                return;
            }

            applying = true;
            fromPicker.Value = range.Value.From;
            toPicker.Value = range.Value.To;
            applying = false;
        };

        void SwitchToCustom(object? sender, EventArgs e)
        {
            if (!applying)
            {
                periodCombo.SelectedItem = Custom;
            }
        }

        fromPicker.ValueChanged += SwitchToCustom;
        toPicker.ValueChanged += SwitchToCustom;

        periodCombo.SelectedItem = initialPreset;
    }

    /// <summary>Standard look for report date pickers.</summary>
    public static void ConfigurePicker(DateTimePicker picker)
    {
        picker.Format = DateTimePickerFormat.Custom;
        picker.CustomFormat = UiFormats.Date;
        picker.MinDate = ValidationHelper.MinimumDate;
        picker.MaxDate = ValidationHelper.MaximumDate;
    }
}
