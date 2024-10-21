using LForms.Extensions;
using System.Drawing;
using System.Windows.Forms;

namespace LForms.Controls.Base;

/// <summary>
/// Represents a customizable base button with predefined styles and behaviors.
/// </summary>
public abstract class LealBaseButton : Button
{
    /// <summary>
    /// Set if the region is rounded
    /// </summary>
    protected bool _roundedRegion = false;

    /// <summary>
    /// Set the smootheness of the region
    /// </summary>
    protected int _regionSmoothness = LealConstants.ELIPSE_CURVE;

    private int _borderSize;

    /// <summary>
    /// Initializes a new instance of the <see cref="LealBaseButton"/> class with default settings.
    /// </summary>
    protected LealBaseButton()
    {
        Text = "LealButton";
        Cursor = Cursors.Hand;
        ForeColor = Color.Black;
        FlatAppearance.BorderColor = ForeColor;
        Size = new Size(200, 50);
        FlatStyle = FlatStyle.Flat;
        Font = new Font("Arial", 12, FontStyle.Regular);
        DoubleBuffered = true;
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        Resize += LealBaseButton_Resize;
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
        set
        {
            FlatAppearance.BorderSize = value;
            ReDraw();
        }
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

    /// <inheritdoc/>
    protected override bool ShowFocusCues
    {
        get => false;
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
        {
            _borderSize = FlatAppearance.BorderSize;
            FlatAppearance.BorderSize = 0;
            this.GenerateRoundRegion(_regionSmoothness);
        }
        else
        {
            Region = null;
            FlatAppearance.BorderSize = _borderSize;
        }
    }

    private void LealBaseButton_Resize(object? sender, System.EventArgs e)
    {
        ReDraw();
        UpdateRegion();
    }
}