﻿using LForms.Enums.MessageBox;
using LForms.Models;
using System.Drawing;
using System.Windows.Forms;

namespace LForms.Controls.MessageBox;

public static class LealMessageBox
{
    public static DialogResult Show(string? title, string? message) 
        => DisplayMessage(title, message, IconType.None, [LealMessageBoxButton.Ok]);

    private static DialogResult DisplayMessage(string? title, string? message, IconType iconType, LealMessageBoxButton[] dialogButtons)
    {
        var messageBoxForm = new LealMessageDisplay(new Size(600, 400), tryDarkMode: true)
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