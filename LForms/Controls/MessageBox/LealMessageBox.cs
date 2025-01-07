using LForms.Enums.MessageBox;
using LForms.Models;
using System.Drawing;
using System.Windows.Forms;

namespace LForms.Controls.MessageBox;

/// <summary>
/// Provides methods to display a customizable message box dialog with various options 
/// including title, message, icons, and buttons.
/// </summary>
public static class LealMessageBox
{
    /// <summary>
    /// Displays a customizable message box with the specified configuration, using a default size.
    /// </summary>
    /// <param name="title">The title of the message box. If null, no title will be displayed.</param>
    /// <param name="message">The main message to display in the message box. If null, no message will be displayed.</param>
    /// <param name="iconType">Specifies the icon to display in the message box.</param>
    /// <param name="dialogButtons">
    /// An array of <see cref="LealMessageBoxButton"/> representing the buttons to display in the message box.
    /// </param>
    /// <param name="buttonFlatStyle">
    /// The flat style for the buttons displayed in the message box. Determines the appearance of the buttons.
    /// </param>
    /// <param name="pacingBetweenButtons">
    /// The spacing (in pixels) between buttons in the message box. Default value is 15.
    /// </param>
    /// <param name="startPosition">
    /// Specifies the starting position of the message box on the screen. Default is <see cref="FormStartPosition.CenterScreen"/>.
    /// </param>
    /// <returns>
    /// A <see cref="DialogResult"/> representing the result of the message box interaction, indicating which button was clicked.
    /// </returns>
    public static DialogResult Show(
        string? title,
        string? message,
        IconType iconType,
        LealMessageBoxButton[] dialogButtons,
        FlatStyle buttonFlatStyle,
        int pacingBetweenButtons = 15,
        FormStartPosition startPosition = FormStartPosition.CenterScreen
    ) => Show(title, message, iconType, null, dialogButtons, buttonFlatStyle, pacingBetweenButtons, startPosition);

    /// <summary>
    /// Displays a customizable message box with the specified configuration.
    /// </summary>
    /// <param name="title">The title of the message box. If null, no title will be displayed.</param>
    /// <param name="message">The main message to display in the message box. If null, no message will be displayed.</param>
    /// <param name="iconType">Specifies the icon to display in the message box.</param>
    /// <param name="size">The size of the message box. If null, a default size will be used.</param>
    /// <param name="dialogButtons">
    /// An array of <see cref="LealMessageBoxButton"/> representing the buttons to display in the message box.
    /// </param>
    /// <param name="buttonFlatStyle">
    /// The flat style for the buttons displayed in the message box. Determines the appearance of the buttons.
    /// </param>
    /// <param name="pacingBetweenButtons">
    /// The spacing (in pixels) between buttons in the message box. Default value is 15.
    /// </param>
    /// <param name="startPosition">
    /// Specifies the starting position of the message box on the screen. Default is <see cref="FormStartPosition.CenterScreen"/>.
    /// </param>
    /// <returns>
    /// A <see cref="DialogResult"/> representing the result of the message box interaction, indicating which button was clicked.
    /// </returns>
    public static DialogResult Show(
        string? title,
        string? message,
        IconType iconType,
        Size? size,
        LealMessageBoxButton[] dialogButtons,
        FlatStyle buttonFlatStyle,
        int pacingBetweenButtons = 15,
        FormStartPosition startPosition = FormStartPosition.CenterScreen
        )
    {
        var messageBoxForm = new LealMessageDisplay(size, startPosition)
        {
            Text = title,
            Message = message,
            Font = new Font("Rubik", 12),
            IconType = iconType,
            Spacing = pacingBetweenButtons,
            ButtonFlatStyle = buttonFlatStyle,
            LealMessageBoxButtons = [.. dialogButtons],
        };
        return messageBoxForm.ShowDialog();
    }
}