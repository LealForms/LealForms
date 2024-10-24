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
    private LealButton? _closeButton;
    private LealButton? _colorPicker;

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

        var backPanel = new LealResizablePanel()
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White.Darken(0.7)
        };
        this.Add(backPanel);

        _topPanel = new LealPanel(true)
        {
            Height = 50,
            Dock = DockStyle.Top,
            BackColor = Color.Yellow.Darken(0.1)
        };
        backPanel.Add(_topPanel);

        _closeButton = new LealButton((s, e) => { Close(); })
        {
            Text = "X",
            BorderSize = 0,
            Width = _topPanel.Height,
            Dock = DockStyle.Right,
            MouseHoverColor = Color.Red,
            MouseDownColor = Color.Red,
            ForeColor = Color.WhiteSmoke,
            Font = new Font("Lucida Console", 14, FontStyle.Regular)
        };
        _topPanel.Add(_closeButton);

        var colorMenu = new ContextMenuStrip();
        colorMenu.Items.Add(null, new SolidBrush().GenerateFilledImage(15, 15), (s, e) => { _topPanel.BackColor = Color.Red; });
        colorMenu.Items.Add(null, new SolidBrush(Color.FromArgb(230, 185, 5)).GenerateFilledImage(15, 15), (s, e) => { _topPanel.BackColor = Color.Green; });
        colorMenu.Items.Add(null, new SolidBrush(Color.FromArgb(230, 185, 5)).GenerateFilledImage(15, 15), (s, e) => { _topPanel.BackColor = Color.Blue; });

        var col = new SolidBrush(Color.FromArgb(230, 185, 5));

        _colorPicker = new LealButton((s, e) => ColorChooser(colorMenu))
        {
            Text = "C",
            BorderSize = 0,
            Width = _topPanel.Height,
            Dock = DockStyle.Left,
            TextAlign = ContentAlignment.TopLeft,
            ForeColor = Color.WhiteSmoke,
            Font = new Font("Lucida Console", 8, FontStyle.Regular)
        };

        _topPanel.Add(_colorPicker);

        this.Activated += (s, e) =>
        {
            _topPanel.Height = 50;
            _closeButton.Visible = true;
            _colorPicker.Visible = true;
        };
        this.Deactivate += (s, e) =>
        {
            _topPanel.Height = 5;
            _closeButton.Visible = false;
            _colorPicker.Visible = true;
        };
    }

    private void ColorChooser(ContextMenuStrip menu)
    {
        var location = _colorPicker.PointToScreen(new Point(0, _colorPicker.Height));
        menu.Show(location);
    }
}