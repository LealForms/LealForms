using LForms.Controls.Panels;
using LForms.Enums.Checkbox;
using LForms.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LForms.Controls.Mischellaneous;

/// <summary>
/// Represents a customizable checkbox component with options for appearance, size, and label alignment.
/// </summary>
public class LealCheckbox : LealPanel
{
    private int _gap = 5;
    private int _boxSize = 20;
    private bool _rounded = false;
    private bool _checked = false;
    private bool _autoSize = true;
    private Color _uncheckedColor = Color.White;
    private Color _checkedColor = Color.Blue;
    private CheckboxStyle _checkboxType = CheckboxStyle.Square;
    private CheckboxCharacterType _checkboxCharacterType = CheckboxCharacterType.CheckMark;
    private CheckboxLabelAlignment _checkboxLabelAlignment = CheckboxLabelAlignment.CheckBoxLeftLabelRight;

    private readonly Label _label;
    private readonly LealPanel _clickableContainer;

    private Control _checkbox;
    private readonly Label _checkedLabel;

    /// <summary>
    /// Initializes a new instance of the <see cref="LealCheckbox"/> class with default settings.
    /// </summary>
    public LealCheckbox() : base(false, true)
    {
        BackColor = Color.Transparent;

        _clickableContainer = new LealPanel(false, true)
        {
            BackColor = Color.Transparent,
        };
        this.Add(_clickableContainer);

        _checkbox = new LealPanel()
        {
            BackColor = _uncheckedColor,
            Size = new Size(_boxSize, _boxSize),
        };
        _checkbox.Click += CheckBox_Click;
        _clickableContainer.Add(_checkbox);

        _checkedLabel = new Label()
        {
            Text = "✓",
            Visible = false,
            AutoSize = false,
            Dock = DockStyle.Fill,
            ForeColor = Color.White,
            Font = new Font("Rubik", 10, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
        };
        _checkedLabel.Click += CheckBox_Click;
        _checkbox.Add(_checkedLabel);

        _label = new Label()
        {
            AutoSize = false,
            Text = "LealCheckbox",
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font("Rubik", 10, FontStyle.Regular),
        };
        this.Add(_label);
    }

    /// <summary>
    /// Gets the label associated with the checkbox, allowing customization of its text, font, and alignment.
    /// </summary>
    public Label Label => _label;

    /// <summary>
    /// Gets or sets the text displayed in the checkbox label.
    /// </summary>
    public new string Text
    {
        get => _label.Text;
        set
        {
            _label.Text = value;
        }
    }

    /// <summary>
    /// Gets or sets the foreground color of the checkbox label.
    /// </summary>
    public new Color ForeColor
    {
        get => _label.ForeColor;
        set => _label.ForeColor = value;
    }

    /// <summary>
    /// Gets or sets the color of the checkmark symbol when the checkbox is selected.
    /// </summary>
    public Color MarkColor
    {
        get => _checkedLabel.ForeColor;
        set => _checkedLabel.ForeColor = value;
    }

    /// <summary>
    /// Gets or sets the character displayed within the checkbox when checked, such as a check mark, cross, or dot.
    /// </summary>
    public CheckboxCharacterType CharacterType
    {
        get => _checkboxCharacterType;
        set
        {
            _checkboxCharacterType = value;

            _checkedLabel.Text = value switch
            {
                CheckboxCharacterType.CheckMark => "✓",
                CheckboxCharacterType.CrossMark => "✗",
                CheckboxCharacterType.DotMark => "•",
                CheckboxCharacterType.StarMark => "★",
                CheckboxCharacterType.HeartMark => "❤",
                CheckboxCharacterType.SquareMark => "■",
                CheckboxCharacterType.CircleMark => "●",
                CheckboxCharacterType.TriangleMark => "▲",
                CheckboxCharacterType.None => string.Empty,
                _ => string.Empty,
            };

            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets the background color of the checkbox when unchecked.
    /// </summary>
    public Color UnCheckedColor
    {
        get => _uncheckedColor;
        set
        {
            _uncheckedColor = value;
            if (!Checked)
                _checkbox.BackColor = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets the background color of the checkbox when checked.
    /// </summary>
    public Color CheckedColor
    {
        get => _checkedColor;
        set
        {
            _checkedColor = value;
            if (Checked)
                _checkbox.BackColor = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the checkbox is checked.
    /// </summary>
    public bool Checked
    {
        get => _checked;
        set
        {
            _checked = value;
            _checkedLabel.Visible = value;
            _checkbox.BackColor = value ? _checkedColor : _uncheckedColor;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the checkbox has rounded corners.
    /// </summary>
    public bool Rounded
    {
        get => _rounded;
        set
        {
            _rounded = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the checkbox is autosized.
    /// </summary>
    public new bool AutoSize
    {
        get => _autoSize;
        set
        {
            _autoSize = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets the gap (in pixels) between the checkbox and its label.
    /// </summary>
    public int Gap
    {
        get => _gap;
        set
        {
            _gap = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets the size (in pixels) of the checkbox box.
    /// </summary>
    public int BoxSize
    {
        get => _boxSize;
        set
        {
            _boxSize = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets the alignment of the checkbox relative to its label.
    /// </summary>
    public CheckboxLabelAlignment CheckboxLabelAlignment
    {
        get => _checkboxLabelAlignment;
        set
        {
            _checkboxLabelAlignment = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets the shape of the checkbox, allowing either a square or circular style.
    /// </summary>
    public CheckboxStyle CheckboxStyle
    {
        get => _checkboxType;
        set
        {
            if (_checkboxType == value)
                return;

            _checkboxType = value;
            _clickableContainer.Remove(_checkbox);
            _checkbox.Click -= CheckBox_Click;
            _checkbox.Remove(_checkedLabel);
            _checkbox.Dispose();

            _checkbox = value == CheckboxStyle.Square 
                ? new LealPanel() { BackColor = Color.White, Size = new Size(_boxSize, _boxSize), }
                : new LealCircularPanel(false, _boxSize) { BackColor = Color.White };

            _clickableContainer.Add(_checkbox);
            _checkbox.Click += CheckBox_Click;
            _checkbox.Add(_checkedLabel);

            ReDraw();
        }
    }

    /// <inheritdoc/>
    protected override void ReDraw()
    {
        int width;
        int height;

        switch (_checkboxLabelAlignment)
        {
            case CheckboxLabelAlignment.CheckBoxLeftLabelRight:
                width = _checkbox.Width + _gap + _label.Text.GetTextSize(_label.Font).Width;
                height = Math.Max(_checkbox.Height, _label.Text.GetTextSize(_label.Font).Height);
                MinimumSize = new Size(width + 5, height);

                _clickableContainer.Height = height;
                _clickableContainer.Width = (int)(width * 0.3);
                _clickableContainer.DockTopBottomLeftWithPadding(0, 0, 0);
                _label.DockFillWithPadding(_clickableContainer.Width + _gap, 0, 0, 0);
                break;
            case CheckboxLabelAlignment.CheckBoxRightLabelLeft:
                width = _checkbox.Width + _gap + _label.Text.GetTextSize(_label.Font).Width;
                height = Math.Max(_checkbox.Height, _label.Text.GetTextSize(_label.Font).Height);
                MinimumSize = new Size(width + 5, height);

                _clickableContainer.Height = height;
                _clickableContainer.Width = (int)(width * 0.3);
                _clickableContainer.DockTopBottomRightWithPadding(0, 0, 0);
                _label.DockFillWithPadding(0, _clickableContainer.Width + _gap, 0, 0);
                break;
            case CheckboxLabelAlignment.CheckBoxTopLabelBottom:
                width = _label.Text.GetTextSize(_label.Font).Width;
                height = _checkbox.Height + _gap + _label.Text.GetTextSize(_label.Font).Height;
                MinimumSize = new Size(width, height + 5);

                _clickableContainer.Height = _checkbox.Height;
                _clickableContainer.DockTopLeftRightWithPadding(0, 0, 0);
                _label.DockFillWithPadding(0, 0, 0, _clickableContainer.Height + _gap);
                break;
            case CheckboxLabelAlignment.CheckBoxBottomLabelTop:
                width = _label.Text.GetTextSize(_label.Font).Width;
                height = _checkbox.Height + _gap + _label.Text.GetTextSize(_label.Font).Height;
                MinimumSize = new Size(width, height + 5);

                _clickableContainer.Height = _checkbox.Height;
                _clickableContainer.DockBottomLeftRightWithPadding(0, 0, 0);
                _label.DockFillWithPadding(0, 0, _clickableContainer.Height + _gap, 0);
                break;
            default: break;
        }

        _checkbox.Centralize();

        if (_autoSize)
            Size = MinimumSize;

        if (_rounded)
            this.GenerateRoundRegion();
        else
            Region = null;
    }

    /// <inheritdoc/>
    protected override void LoadComponents() => ReDraw();
    
    /// <summary>
    /// Toggles the checkbox's checked state when clicked.
    /// </summary>
    /// <param name="sender">The source of the click event.</param>
    /// <param name="e">The event data.</param>
    private void CheckBox_Click(object? sender, EventArgs e) => Checked = !Checked;
}