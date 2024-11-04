using LForms.Controls.Buttons;
using LForms.Controls.Forms;
using LForms.Controls.Panels;
using LForms.Controls.TextBoxes;
using LForms.Extensions;
using System.Drawing;
using System.Windows.Forms;

namespace LForms.Samples.StickyNotes;

public sealed class Note : LealForm
{
    private LealPanel? _topPanel;
    private LealButton? _closeButton;
    private LealButton? _colorPicker;
    private LealTextBox? _textBox;

    private readonly bool _load = false;
    private readonly Color? _starterColor;
    private readonly string? _starterText;

    public delegate void OnClosingStickyNote(Color color, string? text);
    public event OnClosingStickyNote? StickyNoteClose;

    public Note(Form owner, Color? startColor = null, string? text = null)
    {
        Owner = owner;
        _starterColor = startColor;
        _starterText = text;

        _load = true;
        LoadComponents();
    }

    public override void ReDraw()
    {
        Invalidate();
    }

    public override void LoadComponents()
    {
        if (!_load)
            return;

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
        if (_starterColor.HasValue)
            _topPanel.BackColor = _starterColor.Value;

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

        _textBox = new LealTextBox()
        {
            Text = _starterText,
            Multiline = true,
            ForeColor = Color.WhiteSmoke,
        };
        backPanel.Add(_textBox);

        StickyActivation(true);
        Activated += (s, e) => StickyActivation(true);
        Deactivate += (s, e) => StickyActivation(false);
        FormClosed += (s, e) => StickyNoteClose?.Invoke(_topPanel.BackColor, _textBox.Text);
    }

    private void StickyActivation(bool activation)
    {
        _topPanel!.Height = activation ? 50 : 10;
        _closeButton!.Visible = activation;
        _colorPicker!.Visible = activation;
        _textBox!.DockFillWithPadding(LealConstants.C_GRIP, LealConstants.C_GRIP, LealConstants.C_GRIP, _topPanel.Height + LealConstants.C_GRIP);
    }

    private void ColorChooser()
    {
        var modal = new ColorModal(this, new Size(350, 50), _topPanel!.BackColor)
        {
            MinimumSize = new Size(350, 50)
        };
        modal.ColorChanged += (s, e) => _topPanel!.BackColor = e;
        modal.ShowDialog();
    }
}