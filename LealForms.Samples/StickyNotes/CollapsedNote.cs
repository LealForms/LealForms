﻿using LealForms.Controls.Panels;
using LealForms.Extensions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LealForms.Samples.StickyNotes;

public class CollapsedNote : LealPanel
{
    public event EventHandler<string>? OpenNote;
    public event EventHandler? CloseNote;

    public CollapsedNote(string text, Color color)
    {
        Text = text;
        Color = color;
        DateCreated = DateTime.Now;
    }

    public new string Text { get; }
    public Color Color { get; }
    public DateTime DateCreated { get; }

    protected override void LoadComponents()
    {
        Height = 100;
        BackColor = Color.FromArgb(64, 64, 64);
        ForeColor = Color.FromArgb(181, 181, 181);

        var separator = new LealPanel()
        {
            Height = 5,
            BackColor = Color,
            Dock = DockStyle.Top,
        };
        separator.MouseDown += CallapsedNote_Open;

        var timeLabel = new Label()
        {
            Text = $"{DateTime.Now:t}",
            Height = 20,
            AutoSize = false,
            Dock = DockStyle.Top,
            TextAlign = ContentAlignment.MiddleRight,
            Font = new Font("Arial", 10, FontStyle.Regular),
        };
        timeLabel.MouseDown += CallapsedNote_Open;

        var label = new Label()
        {
            Text = Text,
            AutoSize = false,
            Dock = DockStyle.Fill,
            ForeColor = Color.WhiteSmoke,
            TextAlign = ContentAlignment.TopLeft,
        };
        label.MouseDown += CallapsedNote_Open;

        this.Add(label);
        this.Add(timeLabel);
        this.Add(separator);
    }

    private void CallapsedNote_Open(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            OpenNote?.Invoke(this, Text);
        else if (e.Button == MouseButtons.Right)
            CloseNote?.Invoke(this, e);
    }
}