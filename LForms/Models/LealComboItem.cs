namespace LForms.Models;

/// <summary>
/// Represents an item in a combo box, with a display text and an associated value.
/// </summary>
public record LealComboItem
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LealComboItem"/> record with the specified text and value.
    /// </summary>
    /// <param name="text">The display text for the combo box item.</param>
    /// <param name="value">The associated value of the combo box item.</param>
    public LealComboItem(string text, object value)
    {
        Text = text;
        Value = value;
    }

    /// <summary>
    /// Gets the display text for the combo box item.
    /// </summary>
    public string Text { get; init; }

    /// <summary>
    /// Gets the associated value of the combo box item.
    /// </summary>
    public object Value { get; init; }

    /// <inheritdoc/>
    public override string ToString() => Text;
}