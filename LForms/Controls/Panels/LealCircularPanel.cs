using LealForms.Extensions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LealForms.Controls.Panels;

/// <summary>
/// Represents a circular panel that maintains an equal width and height, ensuring a circular appearance.
/// Inherits from <see cref="LealPanel"/> and applies custom rounded edges.
/// </summary>
public class LealCircularPanel : LealPanel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LealCircularPanel"/> class with specified drag state and size.
    /// </summary>
    /// <param name="isPanelDragged">Indicates if the panel can be dragged.</param>
    /// <param name="size">The initial width and height of the panel.</param>
    /// <remarks>
    /// Adjusts the width or height to ensure they remain equal, preserving a square shape.
    /// </remarks>
    public LealCircularPanel(bool isPanelDragged = false, int size = 100) : base(isPanelDragged, true)
    {
        Size = new Size(size, size);
    }

    /// <inheritdoc/>
    protected override void ReDraw() => GrantRectagleSize();

    /// <inheritdoc/>
    protected override void LoadComponents() => ReDraw();

    /// <summary>
    /// Handles the paint event of the panel, ensuring the panel is drawn as a circle.
    /// </summary>
    /// <param name="e">A <see cref="PaintEventArgs"/> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        using var path = new GraphicsPath();
        path.AddEllipse(0, 0, Width, Height);
        Region = new Region(path);
    }

    /// <summary>
    /// Adjusts the width or height to ensure they remain equal, preserving a square shape.
    /// </summary>
    private void GrantRectagleSize()
    {
        if (Width != Height)
        {
            if (Width > Height)
            {
                Width = Height;
            }
            else
            {
                Height = Width;
            }
        }
    }
}