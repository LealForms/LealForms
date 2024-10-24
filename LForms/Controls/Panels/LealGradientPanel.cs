using LForms.Enums;
using LForms.Extensions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LForms.Controls.Panels;

/// <summary>
/// This control displays a gradient background by blending the specified corner colors.
/// Generating the gradient can be resource-intensive; frequent updates (e.g., resizing) may affect performance.
/// </summary>
public class LealGradientPanel : LealPanel
{
    private Color _colorTopLeft = Color.White;
    private Color _colorTopRight = Color.White;
    private Color _colorBottomRight = Color.White;
    private Color _colorBottomLeft = Color.White;

    private GradientRenderingPriority _renderingPriority = GradientRenderingPriority.Balanced;

    /// <summary>
    /// Initializes a new instance of the <see cref="LealGradientPanel"/> class.
    /// </summary>
    /// <param name="isPanelDragged">set up event handler for dragging the parent form functionality.</param>
    /// <remarks>
    /// This control displays a gradient background by blending the specified corner colors.
    /// Generating the gradient can be resource-intensive; frequent updates (e.g., resizing) may affect performance.
    /// </remarks>
    public LealGradientPanel(bool isPanelDragged = false) : base(isPanelDragged)
    {
        SetStyle(ControlStyles.ResizeRedraw, true);
    }

    /// <summary>
    /// Gets or sets the gradient color at the top-left corner of the panel.
    /// </summary>
    [Category("Appearance")]
    [Description("The gradient color at the top-left corner of the panel.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public Color TopLeftGradientColor
    {
        get => _colorTopLeft;
        set
        {
            if (_colorTopLeft != value)
            {
                _colorTopLeft = value;
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets the gradient color at the top-right corner of the panel.
    /// </summary>
    [Category("Appearance")]
    [Description("The gradient color at the top-right corner of the panel.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public Color TopRightGradientColor
    {
        get => _colorTopRight;
        set
        {
            if (_colorTopRight != value)
            {
                _colorTopRight = value;
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets the gradient color at the bottom-left corner of the panel.
    /// </summary>
    [Category("Appearance")]
    [Description("The gradient color at the bottom-left corner of the panel.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public Color BottomLeftGradientColor
    {
        get => _colorBottomLeft;
        set
        {
            if (_colorBottomLeft != value)
            {
                _colorBottomLeft = value;
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets the gradient color at the bottom-right corner of the panel.
    /// </summary>
    [Category("Appearance")]
    [Description("The gradient color at the bottom-right corner of the panel.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public Color BottomRightGradientColor
    {
        get => _colorBottomRight;
        set
        {
            if (_colorBottomRight != value)
            {
                _colorBottomRight = value;
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Gets or sets the rendering priority for the gradient, balancing quality and performance.
    /// </summary>
    [Category("Behavior")]
    [Description("Specifies the rendering priority for the gradient, balancing quality and performance.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public GradientRenderingPriority RenderingPriority
    {
        get => _renderingPriority;
        set
        {
            if (_renderingPriority != value)
            {
                _renderingPriority = value;
                Invalidate();
            }
        }
    }

    /// <summary>
    /// Overrides the OnPaint method to draw the gradient background.
    /// </summary>
    /// <param name="e">Paint event arguments.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        int gradientWidth;
        int gradientHeight;
        bool highQualityResize = false;

        switch (_renderingPriority)
        {
            case GradientRenderingPriority.HighSpeed:
                gradientWidth = Math.Max(ClientSize.Width / 4, 1);
                gradientHeight = Math.Max(ClientSize.Height / 4, 1);
                break;
            case GradientRenderingPriority.Balanced:
                gradientWidth = Math.Max(ClientSize.Width / 2, 1);
                gradientHeight = Math.Max(ClientSize.Height / 2, 1);
                break;
            case GradientRenderingPriority.HighQuality:
            default:
                gradientWidth = Math.Max(ClientSize.Width, 1);
                gradientHeight = Math.Max(ClientSize.Height, 1);
                highQualityResize = true;
                break;
        }

        var rect = new Rectangle(0, 0, gradientWidth, gradientHeight);

        using var gradientBitmap = rect.Gradient2D(_colorTopLeft, _colorTopRight, _colorBottomRight, _colorBottomLeft);

        if (gradientWidth != ClientSize.Width || gradientHeight != ClientSize.Height)
        {
            // Resize the bitmap to the control's size
            using var resizedBitmap = gradientBitmap.ResizeImage(ClientSize.Width, ClientSize.Height, highQualityResize);

            // Draw the resized bitmap onto the control
            e.Graphics.DrawImage(resizedBitmap, new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
        }
        else
        {
            // Draw the gradient bitmap directly onto the control
            e.Graphics.DrawImage(gradientBitmap, new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void ReDraw() { }
}
