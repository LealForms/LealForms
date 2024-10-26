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
    }

    public override void LoadComponents()
    {
        Size = new Size(350, 300);
        MinimumSize = Size;
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.CenterScreen;

        var backPanel = new LealResizablePanel()
        {
            Dock = DockStyle.Fill,
            BackColor = StickyColors.StickyBackColor,
        };
        this.Add(backPanel);

        _topPanel = new LealPanel(true)
        {
            Height = 50,
            Dock = DockStyle.Top,
            BackColor = StickyColors.PastelYellow
        };
        backPanel.Add(_topPanel);

        _closeButton = new LealButton((s, e) => { Close(); })
        {
            Text = "X",
            BorderSize = 0,
            Width = _topPanel.Height,
            Dock = DockStyle.Right,
            ForeColor = StickyColors.TextBackColor,
            Font = new Font("Lucida Console", 14, FontStyle.Regular)
        };
        _topPanel.Add(_closeButton);

        _colorPicker = new LealButton((s, e) => ColorChooser())
        {
            Text = "C",
            BorderSize = 0,
            Width = _topPanel.Height,
            Dock = DockStyle.Left,
            TextAlign = ContentAlignment.TopLeft,
            ForeColor = StickyColors.TextBackColor,
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

    private void ColorChooser()
    {
        var modal = new LealForm()
        {
            FormBorderStyle = FormBorderStyle.None,
            StartPosition = FormStartPosition.Manual,
            ShowInTaskbar = false,
            Size = new Size(350, 50),
            BackColor = Color.WhiteSmoke,
            Owner = this,
        };

        var screenLocation = this.PointToScreen(new Point(0, 0));
        modal.Location = screenLocation;

        // Add a panel to hold the buttons
        var panelColors = new LealPanel()
        {
            Height = 50,
            Dock = DockStyle.Top,
            BackColor = Color.LightGray
        };
        modal.Add(panelColors);

        var colors = StickyColors.PastelColors;

        foreach (var color in colors)
        {
            var button = GenerateColorChoiceButton(color, color == _topPanel!.BackColor, (s, e) =>
            {
                _topPanel.BackColor = color;
                modal.Close();
            });
            panelColors.Add(button);
        }

        // Show the dialog modally over the current form
        modal.ShowDialog(this);
    }

    private static LealButton GenerateColorChoiceButton(Color color, bool selected, EventHandler onclickHandler) => new(onclickHandler)
    {
        Text = selected ? "✓" : "",
        Width = 50,
        BorderSize = 0,
        BackColor = color,
        MouseHoverColor = color.Darken(0.2),
        MouseDownColor = color,
        Dock = DockStyle.Left,
    };
}