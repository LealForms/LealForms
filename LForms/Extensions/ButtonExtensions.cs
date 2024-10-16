using System.Windows.Forms;

namespace LForms.Extensions;

/// <summary>
/// Custom extension functions for <see cref="Button"/>s
/// </summary>
public static class ButtonExtensions
{
    /// <summary>
    /// Sets the button width automatically based on its text and font, with optional lateral space.
    /// </summary>
    /// <param name="button">The button to adjust.</param>
    /// <param name="lateralSpace">Additional space on the sides of the text.</param>
    public static void SetAutoWidth(this Button button, int lateralSpace = 20)
        => button.Width = TextRenderer.MeasureText(button.Text, button.Font).Width + lateralSpace;
}