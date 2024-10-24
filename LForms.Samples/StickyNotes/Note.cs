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

public sealed class Note : LealForm
{
    private LealPanel? _topPanel;

    public Note(Form owner)
    {
        Owner = owner;
    }

    public override void ReDraw()
    {
        Invalidate();
        this.GenerateRoundRegion();
    }

    public override void LoadComponents()
    {
        Size = new Size(250, 250);
        MinimumSize = Size;
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.CenterScreen;
        LostFocus += Note_LostFocus;

        var backPanel = new LealResizablePanel()
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White.Darken(0.7)
        };
        backPanel.Click += Note_GotFocus;
        this.Add(backPanel);

        _topPanel = new LealPanel(true)
        {
            Height = 50,
            Dock = DockStyle.Top,
            BackColor = Color.Yellow.Darken(0.1)
        };
        _topPanel.Click += Note_GotFocus;
        backPanel.Add(_topPanel);

        var closeButton = new LealButton((s, e) => { Close(); })
        {
            Text = "X",
            Width = _topPanel.Height,
            Dock = DockStyle.Right,
            MouseHoverColor = Color.Red,
            MouseDownColor = Color.Red,
            ForeColor = Color.WhiteSmoke,
            Font = new Font("Lucida Console", 14, FontStyle.Regular)
        };
        _topPanel.Add(closeButton);
    }

    private void Note_LostFocus(object? sender, EventArgs e)
    {
        if (_topPanel == null)
            return;

        _topPanel.Height = 5;
    }

    private void Note_GotFocus(object? sender, EventArgs e)
    {
        if (_topPanel == null)
            return;

        _topPanel.Height = 50;
    }
}