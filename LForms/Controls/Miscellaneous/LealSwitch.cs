using LForms.Controls.Panels;
using LForms.Enums.Switch;
using LForms.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LForms.Controls.Miscellaneous;

public class LealSwitch : LealPanel
{
    public event EventHandler<bool>? StateChanged;

    private int _switchPadding = 1;
    private bool _checked = false;
    private bool _autoSize = true;
    private string _onText = "On";
    private string _offText = "Off";
    private Color _switchColorOn = Color.Blue;
    private Color _switchColorOff = Color.Gray;
    private SwitchStyle _switchStyle = 0;
    private SwitchOrientation _orientation = 0;

    private readonly LealPanel _switchPanel;
    private readonly Label _labelText;

    public LealSwitch() : base(false, true)
    {
        _switchPanel = new LealPanel(false, true)
        {
            BackColor = _switchColorOff,
        };
        _labelText = new Label()
        {
            Text = _onText,
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Rubik", 11, FontStyle.Regular),
        };
    }

    public int SwitchPadding
    {
        get => _switchPadding;
        set
        {
            _switchPadding = value;
            ReDraw();
        }
    }

    public bool Checked
    {
        get => _checked;
        set
        {
            _checked = value;
            ReDraw();
        }
    }

    public bool ShowText
    {
        get => _labelText.Visible;
        set => _labelText.Visible = value;
    }

    public bool AutoSize
    {
        get => _autoSize;
        set
        {
            _autoSize = value;
            ReDraw();
        }
    }

    public string OnText
    {
        get => _onText;
        set
        {
            _onText = value;
            ReDraw();
        }
    }

    public string OffText
    {
        get => _offText;
        set
        {
            _offText = value;
            ReDraw();
        }
    }

    public Color SwitchColorOn
    {
        get => _switchColorOn;
        set
        {
            _switchColorOn = value;
            ReDraw();
        }
    }

    public Color SwitchColorOff
    {
        get => _switchColorOff;
        set
        {
            _switchColorOff = value;
            ReDraw();
        }
    }

    public Font SwitchFont
    {
        get => _labelText.Font;
        set => _labelText.Font = value;
    }

    public SwitchStyle SwitchStyle
    {
        get => _switchStyle;
        set
        {
            _switchStyle = value;
            ReDraw();
        }
    }

    public SwitchOrientation SwitchOrientation
    {
        get => _orientation;
        set
        {
            _orientation = value;
            ReDraw();
        }
    }

    /// <inheritdoc/>
    protected override void ReDraw()
    {
        var textSize = _labelText.Text.GetTextSize(_labelText.Font);

        _labelText.Text = _checked ? _onText : _offText;
        _switchPanel.BackColor = _checked ? _switchColorOn : _switchColorOff;

        if (_orientation == SwitchOrientation.Horizontal)
        {
            MinimumSize = new Size(textSize.Width * 2 + (_switchPadding * 2) + 2, textSize.Height + (_switchPadding * 2) + 2);
            _switchPanel.DockFillWithPadding(_checked ? Width / 2 : _switchPadding, _checked ? _switchPadding : Width / 2, _switchPadding, _switchPadding);
            _labelText.DockFillWithPadding(_checked ? _switchPadding : Width / 2, _checked ? Width / 2 : _switchPadding, _switchPadding, _switchPadding);
        }
        else // Vertical orientation
        {
            MinimumSize = new Size(textSize.Width + (_switchPadding * 2) + 2, textSize.Height * 2 + (_switchPadding * 2) + 2);
            _switchPanel.DockFillWithPadding(_switchPadding, _switchPadding, _checked ? Height / 2 : _switchPadding, _checked ? _switchPadding : Height / 2);
            _labelText.DockFillWithPadding(_switchPadding, _switchPadding, _checked ? _switchPadding : Height / 2, _checked ? Height / 2 : _switchPadding);
        }

        if (_autoSize)
            Size = MinimumSize;
    }

    /// <inheritdoc/>
    protected override void LoadComponents()
    {
        this.Add(_switchPanel);
        this.Add(_labelText);
        Click += LealSwitch_Click;
        _switchPanel.Click += LealSwitch_Click;
        _labelText.Click += LealSwitch_Click;

        ReDraw();
    }

    private void LealSwitch_Click(object? sender, EventArgs e)
    {
        Checked = !Checked;
        StateChanged?.Invoke(sender, _checked);
    }
}