using LForms.Enums.MessageBox;
using LForms.Models;
using System.Drawing;
using System.Windows.Forms;

namespace LForms.Controls.MessageBox;

public static class LealMessageBox
{
    public static DialogResult Show(string? title, string? message) 
        => Show(title, message, IconType.None, [LealMessageBoxButton.Ok]);

    public static DialogResult Show(string? title, string? message, IconType iconType)
        => Show(title, message, iconType, [LealMessageBoxButton.Ok]);

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