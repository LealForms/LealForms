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
    protected int _regionSmoothness = Constants.ELIPSE_CURVE;

    /// <summary>
    /// Initializes a new instance of the <see cref="LealBaseButton"/> class with default settings.
    /// </summary>
    protected LealBaseButton()
    {
        Text = "LealButton";
        Cursor = Cursors.Hand;
        BackColor = Color.White;
        ForeColor = Color.Black;
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
    /// Forces a redraw of the button, implemented by derived classes.
    /// </summary>
    protected abstract void ReDraw();

    /// <summary>
    /// Updates the button's region to apply the rounded edge effect based on the current settings.
    /// </summary>
    protected void UpdateRegion()
    {
        if (_roundedRegion)
        {
            FlatAppearance.BorderSize = 0;
            this.GenerateRoundRegion(_regionSmoothness);
        }
        else
            Region = null;
    }

    private void LealBaseButton_Resize(object? sender, System.EventArgs e)
    {
        ReDraw();
        UpdateRegion();
    }
}