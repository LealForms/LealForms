using LForms.Controls.Buttons;
using LForms.Extensions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LForms.Controls.Base;

/// <summary>
/// Represents a customizable base button with predefined styles and behaviors.
/// </summary>
public abstract class LealBaseButton : Button
{
    private bool _selectable = false;
    private bool _roundedRegion = false;
    private int _regionSmoothness = LealConstants.ELIPSE_CURVE; 

    /// <summary>
    /// Initializes a new instance of the <see cref="LealBaseButton"/> class with default settings.
    /// </summary>
    protected LealBaseButton()
    {
        Text = "LealButton";
        Cursor = Cursors.Hand;
        ForeColor = Color.Black;
        Size = new Size(200, 50);
        FlatStyle = FlatStyle.Flat;
        Font = new Font("Arial", 12, FontStyle.Regular);

        DoubleBuffered = true;
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.Selectable, _selectable);

        Resize += LealBaseButton_Resize;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LealBaseButton"/> class with a specified click event handler.
    /// </summary>
    /// <param name="onclickHandler">The event handler that will be called when the button is clicked.</param>
    protected LealBaseButton(EventHandler onclickHandler) : this()
    {
        Click += onclickHandler;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the button has rounded edges.
    /// </summary>
    public bool Rounded
    {
        get => _roundedRegion;
        set
        {
            _roundedRegion = value;
            UpdateRegion();
        }
    }

    /// <summary>
    /// Gets or sets the smoothness level of the rounded region. This value is used only when <see cref="Rounded"/> is true.
    /// </summary>
    public int RegionSmoothness
    {
        get => _regionSmoothness;
        set
        {
            _regionSmoothness = value;
            UpdateRegion();
        }
    }

    /// <summary>
    /// Gets or sets the border size off the button.
    /// </summary>
    public int BorderSize
    {
        get => FlatAppearance.BorderSize;
        set => FlatAppearance.BorderSize = value;
    }

    /// <summary>
    /// Gets or sets the border color off the button.
    /// </summary>
    public Color BorderColor
    {
        get => FlatAppearance.BorderColor;
        set => FlatAppearance.BorderColor = value;
    }

    /// <summary>
    /// Gets or sets the color of the button when the mouse cursor is within the bounds of the control.
    /// </summary>
    public Color MouseHoverColor
    {
        get => FlatAppearance.MouseOverBackColor;
        set => FlatAppearance.MouseOverBackColor = value;
    }

    /// <summary>
    /// Gets or sets the color of the button when the mouse cursor is within the bounds of the control and the left button is pressed.
    /// </summary>
    public Color MouseDownColor
    {
        get => FlatAppearance.MouseDownBackColor;
        set => FlatAppearance.MouseDownBackColor = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the button will show it's focus cues on focus.
    /// </summary>
    /// <remarks>
    /// Default = false
    /// </remarks>
    public bool ShowFocusBorder { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether the control can be selected.
    /// </summary>
    /// <value>
    /// <c>true</c> if the control is selectable; otherwise, <c>false</c>.
    /// </value>
    /// <remarks>
    /// When set, this property updates the control's style to reflect its selectable state.
    /// </remarks>
    public bool Selectable
    {
        get => _selectable;
        set
        {
            _selectable = value;
            SetStyle(ControlStyles.Selectable, value);
        }
    }

    /// <inheritdoc/>
    protected override bool ShowFocusCues
    {
        get => ShowFocusBorder;
    }

    /// <inheritdoc/>
    protected override bool ShowKeyboardCues
    {
        get => ShowFocusBorder;
    }

    /// <summary>
    /// Forces a redraw of the button, optionally implemented by derived classes.
    /// </summary>
    protected virtual void ReDraw() { }

    /// <summary>
    /// Updates the button's region to apply the rounded edge effect based on the current settings.
    /// </summary>
    protected void UpdateRegion()
    {
        if (_roundedRegion)
            this.GenerateRoundRegion(_regionSmoothness);
        else
            Region = null;
    }

    private void LealBaseButton_Resize(object? sender, EventArgs e)
    {
        ReDraw();
        UpdateRegion();
    }
}