using System.ComponentModel;

namespace InventoryManagement.Helpers;

/// <summary>
/// A BindingList that supports clicking DataGridView column headers to sort.
/// (The standard BindingList&lt;T&gt; does not implement sorting.)
/// </summary>
public sealed class SortableBindingList<T> : BindingList<T>
{
    private bool _isSorted;
    private ListSortDirection _sortDirection;
    private PropertyDescriptor? _sortProperty;

    public SortableBindingList(IEnumerable<T> items)
        : base(new List<T>(items))
    {
    }

    protected override bool SupportsSortingCore => true;

    protected override bool IsSortedCore => _isSorted;

    protected override ListSortDirection SortDirectionCore => _sortDirection;

    protected override PropertyDescriptor? SortPropertyCore => _sortProperty;

    protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
    {
        if (Items is not List<T> list)
        {
            return;
        }

        int multiplier = direction == ListSortDirection.Ascending ? 1 : -1;
        list.Sort((left, right) => multiplier * Compare(property.GetValue(left), property.GetValue(right)));

        _sortProperty = property;
        _sortDirection = direction;
        _isSorted = true;
        OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    }

    protected override void RemoveSortCore()
    {
        _isSorted = false;
        _sortProperty = null;
    }

    private static int Compare(object? left, object? right)
    {
        if (left is null && right is null)
        {
            return 0;
        }

        if (left is null)
        {
            return -1;
        }

        if (right is null)
        {
            return 1;
        }

        if (left is string leftText && right is string rightText)
        {
            return string.Compare(leftText, rightText, StringComparison.CurrentCultureIgnoreCase);
        }

        return left is IComparable comparable ? comparable.CompareTo(right) : string.Compare(left.ToString(), right.ToString(), StringComparison.CurrentCulture);
    }
}
