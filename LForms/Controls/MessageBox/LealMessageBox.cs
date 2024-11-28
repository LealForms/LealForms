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
    {
        var messageBoxForm = new LealMessageDisplay(new Size(600, 300))
        {
            Text = title,
            Message = message,
            Font = new Font("Rubik", 12),
            IconType = iconType,
            LealMessageBoxButtons = [.. dialogButtons],
        };

        return messageBoxForm.ShowDialog();
    }
}