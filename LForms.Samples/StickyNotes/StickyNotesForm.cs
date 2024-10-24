using LForms.Controls.Buttons;
using LForms.Controls.Forms;
using LForms.Controls.Panels;
using LForms.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LForms.Samples.StickyNotes;

public sealed class StickyNotesForm : LealForm
{
    public List<Note> Notes = [];

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
            MouseHoverColor = Color.Red,
            MouseDownColor = Color.Red,
            ForeColor = Color.WhiteSmoke,
            Font = new Font("Lucida Console", 14, FontStyle.Regular)
        };
        topPanel.Add(addNewButton);
    }

    private void NewStickyNote()
    {
        var newNote = new Note(this);
        newNote.Show();
        Notes.Add(newNote);
    }
}