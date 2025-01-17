using LealForms.Controls.Buttons;
using LealForms.Controls.Forms;
using LealForms.Controls.Panels;
using LealForms.Controls.TextBoxes;
using LealForms.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LealForms.Samples.StickyNotes;

public sealed class StickyNotesForm : LealForm
{
    private Panel? _panelNotes;
    private readonly List<CollapsedNote> Notes = [];

    public override void ReDraw()
    {
        Invalidate();
        this.GenerateRoundRegion();
    }

    public override void LoadComponents()
    {
        Size = new Size(400, 600);
        MinimumSize = Size;
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.CenterScreen;

        var backPanel = new LealResizablePanel()
        {
            Dock = DockStyle.Fill,
            BackColor = Color.FromArgb(32, 32, 32)
        };
        this.Add(backPanel);

        var topPanel = new LealPanel(true)
        {
            Height = 50,
            Dock = DockStyle.Top
        };
        backPanel.Add(topPanel);

        var closeButton = new LealButton((s, e) => { Close(); })
        {
            Text = "X",
            BorderSize = 0,
            Width = topPanel.Height,
            Dock = DockStyle.Right,
            MouseHoverColor = Color.Red,
            MouseDownColor = Color.Red,
            ForeColor = Color.WhiteSmoke,
            Font = new Font("Lucida Console", 14, FontStyle.Regular)
        };
        topPanel.Add(closeButton);

        var addNewButton = new LealButton((s, e) => { NewStickyNote(); })
        {
            Text = "+",
            BorderSize = 0,
            Width = topPanel.Height,
            Dock = DockStyle.Left,
            ForeColor = Color.WhiteSmoke,
            Font = new Font("Lucida Console", 14, FontStyle.Regular)
        };
        topPanel.Add(addNewButton);

        var labelText = new Label()
        {
            Text = "Sticky Notes",
            Height = 35,
            AutoSize = false,
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.WhiteSmoke,
            Font = new Font("Arial", 16, FontStyle.Regular),
            TextAlign = ContentAlignment.MiddleLeft
        };
        backPanel.Add(labelText);
        labelText.DockTopLeftRightWithPadding(topPanel.Height + 5, LealConstants.C_GRIP - 5, LealConstants.C_GRIP);

        var textBoxSearch = new LealTextBox()
        {
            Placeholder = "Search",
            BackColor = Color.FromArgb(64, 64, 64),
            ForeColor = Color.FromArgb(181, 181, 181)
        };
        backPanel.Add(textBoxSearch);
        textBoxSearch.DockTopLeftRightWithPadding(topPanel.Height + labelText.Height + 10, LealConstants.C_GRIP, LealConstants.C_GRIP);
        textBoxSearch.TextChanged += TextBoxSearch_TextChanged;

        _panelNotes = new LealPanel()
        {
            Cursor = Cursors.Default,
            BackColor = Color.FromArgb(32, 32, 32)
        };
        backPanel.Add(_panelNotes);
        _panelNotes.DockFillWithPadding(LealConstants.C_GRIP, LealConstants.C_GRIP, LealConstants.C_GRIP, topPanel.Height + labelText.Height + textBoxSearch.Height + LealConstants.C_GRIP * 2
            );
    }

    private void NewStickyNote(Color? startColor = null, string ? text = null)
    {
        var newNote = new Note(this, startColor, text);
        newNote.StickyNoteClose += StickyNote_StickyNoteClose;
        newNote.Show();
    }

    private void TextBoxSearch_TextChanged(string text, EventArgs e)
    {
        Trace.Assert(_panelNotes != null);

        _panelNotes.Controls.Clear();
        var sortedNotes = Notes.OrderByDescending((x) => x.DateCreated).ToArray();

        if (string.IsNullOrEmpty(text))
            _panelNotes.Controls.AddRange([.. sortedNotes]);
        else
        {
            var notes = sortedNotes
                .Where(x =>  text.Equals(x.Text, StringComparison.CurrentCultureIgnoreCase) || x.Text.Contains(text, StringComparison.CurrentCultureIgnoreCase))
                .ToArray();

            _panelNotes.Controls.AddRange(notes);
        }

        _panelNotes!.WaterFallChildControlsOfTypeByY<CollapsedNote>(0, 10);

        foreach (var note in _panelNotes.Controls)
        {
            if (note is not CollapsedNote nt)
                continue;

            nt.DockTopLeftRightWithPadding(nt.Top, 0, 0);
        }
    }

    private void StickyNote_StickyNoteClose(Color color, string? text)
    {
        if (string.IsNullOrEmpty(text))
            return;

        var collapsedNote = new CollapsedNote(text, color);
        collapsedNote.OpenNote += CollapsedNote_OpenNote;
        collapsedNote.CloseNote += CollapsedNote_CloseNote;
        Notes.Add(collapsedNote);
        TextBoxSearch_TextChanged("", EventArgs.Empty);
    }

    private void CollapsedNote_OpenNote(object? sender, string e)
    {
        if (sender is not CollapsedNote nt)
            return;

        Notes.Remove(nt);
        NewStickyNote(nt.Color, nt.Text);
        nt.Dispose();
    }

    private void CollapsedNote_CloseNote(object? sender, EventArgs e)
    {
        if (sender is not CollapsedNote nt)
            return;

        var dialogResult = MessageBox.Show("Are you sure you want to delete this note?", "Delete Note",
            MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

        if (dialogResult != DialogResult.Yes)
            return;

        Notes.Remove(nt);
        nt.Dispose();
        TextBoxSearch_TextChanged("", EventArgs.Empty);
    }
}