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
    /// Displays a message box with the specified title and message.
    /// </summary>
    /// <param name="title">The title of the message box.</param>
    /// <param name="message">The message to display in the message box.</param>
    /// <returns>
    /// A <see cref="DialogResult"/> indicating the result of the dialog interaction.
    /// </returns>
    public static DialogResult Show(string? title, string? message)
        => Show(title, message, IconType.None, [LealMessageBoxButton.Ok]);

    /// <summary>
    /// Displays a message box with the specified title, message, and icon type.
    /// </summary>
    /// <param name="title">The title of the message box.</param>
    /// <param name="message">The message to display in the message box.</param>
    /// <param name="iconType">
    /// The <see cref="IconType"/> to display in the message box (e.g., Information, Warning, Error).
    /// </param>
    /// <returns>
    /// A <see cref="DialogResult"/> indicating the result of the dialog interaction.
    /// </returns>
    public static DialogResult Show(string? title, string? message, IconType iconType)
        => Show(title, message, iconType, [LealMessageBoxButton.Ok]);

    /// <summary>
    /// Displays a message box with the specified message, icon type, and buttons.
    /// </summary>
    /// <param name="message">The message to display in the message box.</param>
    /// <param name="iconType">
    /// The <see cref="IconType"/> to display in the message box (e.g., Information, Warning, Error).
    /// </param>
    /// <param name="dialogButtons">
    /// An array of <see cref="LealMessageBoxButton"/> objects representing the buttons to display.
    /// </param>
    /// <returns>
    /// A <see cref="DialogResult"/> indicating the result of the dialog interaction.
    /// </returns>
    public static DialogResult Show(string? message, IconType iconType, LealMessageBoxButton[] dialogButtons)
        => Show(null, message, iconType, dialogButtons);

    /// <summary>
    /// Displays a message box with the specified title, message, icon type, and buttons.
    /// </summary>
    /// <param name="title">The title of the message box.</param>
    /// <param name="message">The message to display in the message box.</param>
    /// <param name="iconType">
    /// The <see cref="IconType"/> to display in the message box (e.g., Information, Warning, Error).
    /// </param>
    /// <param name="dialogButtons">
    /// An array of <see cref="LealMessageBoxButton"/> objects representing the buttons to display.
    /// </param>
    /// <returns>
    /// A <see cref="DialogResult"/> indicating the result of the dialog interaction.
    /// </returns>
    public static DialogResult Show(string? title, string? message, IconType iconType, LealMessageBoxButton[] dialogButtons)
        => Show(title, message, iconType, dialogButtons, FlatStyle.Flat);

    /// <summary>
    /// Displays a message box with the specified title, message, icon type, buttons, and button flat style.
    /// </summary>
    /// <param name="title">The title of the message box.</param>
    /// <param name="message">The message to display in the message box.</param>
    /// <param name="iconType">
    /// The <see cref="IconType"/> to display in the message box (e.g., Information, Warning, Error).
    /// </param>
    /// <param name="dialogButtons">
    /// An array of <see cref="LealMessageBoxButton"/> objects representing the buttons to display.
    /// </param>
    /// <param name="buttonFlatStyle">
    /// The <see cref="FlatStyle"/> applied to the buttons in the message box.
    /// </param>
    /// <returns>
    /// A <see cref="DialogResult"/> indicating the result of the dialog interaction.
    /// </returns>
    public static DialogResult Show(string? title, string? message, IconType iconType, LealMessageBoxButton[] dialogButtons, FlatStyle buttonFlatStyle) 
        => Show(title, message, iconType, dialogButtons, buttonFlatStyle, null);

    /// <summary>
    /// Displays a message box with the specified title, message, icon type, buttons, button flat style, and size.
    /// </summary>
    /// <param name="title">The title of the message box.</param>
    /// <param name="message">The message to display in the message box.</param>
    /// <param name="iconType">
    /// The <see cref="IconType"/> to display in the message box (e.g., Information, Warning, Error).
    /// </param>
    /// <param name="dialogButtons">
    /// An array of <see cref="LealMessageBoxButton"/> objects representing the buttons to display.
    /// </param>
    /// <param name="buttonFlatStyle">
    /// The <see cref="FlatStyle"/> applied to the buttons in the message box.
    /// </param>
    /// <param name="size">
    /// The <see cref="Size"/> of the message box. If <c>null</c>, the size will be calculated automatically.
    /// </param>
    /// <returns>
    /// A <see cref="DialogResult"/> indicating the result of the dialog interaction.
    /// </returns>
    public static DialogResult Show(string? title, string? message, IconType iconType, LealMessageBoxButton[] dialogButtons, FlatStyle buttonFlatStyle, Size? size)
    {
        var messageBoxForm = new LealMessageDisplay(size)
        {
            Text = title,
            Message = message,
            Font = new Font("Rubik", 12),
            IconType = iconType,
            ButtonFlatStyle = buttonFlatStyle,
            LealMessageBoxButtons = [.. dialogButtons],
        };
        return messageBoxForm.ShowDialog();
    }
}