using LForms.Controls.Panels;
using LForms.Extensions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LForms.Controls.Mischellaneous;

/// <summary>
/// Represents a custom panel control that draws a separating line with configurable spacing, thickness, color, and orientation.
/// </summary>
public class LealSeparator : LealPanel
{
    private readonly Panel _linedPanel;

    private int _lineSpacing = 5;
    private int _lineThickness = 2;
    private Color _lineColor = Color.Black;
    private Orientation _orientation;

    /// <summary>
    /// Initializes a new instance of the <see cref="LealSeparator"/> class.
    /// </summary>
    public LealSeparator()
    {
        _linedPanel = new Panel();
        Size = new Size(250, 10);
        this.Add(_linedPanel);
        Resize += LealSeparator_Resize;
        ControlAdded += LealSeparator_ControlAdded;
    }

    /// <summary>
    /// Gets or sets the spacing between lines in the separator.
    /// Changing this value triggers a redraw.
    /// </summary>
    /// <value>The spacing between lines, in pixels. The default value is 5.</value>
    [Category("Appearance")]
    [Description("The spacing between lines in the separator, in pixels.")]
    public int LineSpacing
    {
        get => _lineSpacing;
        set
        {
            _lineSpacing = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets the thickness of the separating line.
    /// Changing this value triggers a redraw.
    /// </summary>
    /// <value>The thickness of the line, in pixels. The default value is 2.</value>
    [Category("Appearance")]
    [Description("The thickness of the separating line, in pixels.")]
    public int LineThickness
    {
        get => _lineThickness;
        set 
        { 
            _lineThickness = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets the color of the separating line.
    /// Changing this value triggers a redraw.
    /// </summary>
    /// <value>The color of the line. The default value is <see cref="Color.Black"/>.</value>
    [Category("Appearance")]
    [Description("The color of the separating line.")]
    public Color LineColor
    {
        get => _lineColor;
        set
        {
            _lineColor = value;
            ReDraw();
        }
    }

    /// <summary>
    /// Gets or sets the orientation of the separator (horizontal or vertical).
    /// Changing this value triggers a redraw.
    /// </summary>
    /// <value>An <see cref="Orientation"/> value that indicates whether the line is drawn horizontally or vertically.</value>
    [Category("Layout")]
    [Description("The orientation of the separator, either horizontal or vertical.")]
    public Orientation Orientation
    {
        get => _orientation;
        set
        {
            _orientation = value;
            ReDraw();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void ReDraw()
    {
        if (Orientation == Orientation.Horizontal)
        {
            MinimumSize = new Size(0, _lineThickness + _lineSpacing);
            _linedPanel.Width = Width;
            _linedPanel.Height = _lineThickness;
        }
        else
        {
            MinimumSize = new Size(_lineThickness + _lineSpacing, 0);
            _linedPanel.Height = Height;
            _linedPanel.Width = _lineThickness;
        }

        _linedPanel.CentralizeRelativeTo(this);
        _linedPanel.BackColor = _lineColor;
        Invalidate();
    }

    private void LealSeparator_ControlAdded(object? sender, ControlEventArgs e) => this.Remove(e.Control);

    /// <summary>
    /// Handles the <see cref="Control.Resize"/> event to trigger a redraw of the separator when the control is resized.
    /// </summary>
    private void LealSeparator_Resize(object? sender, EventArgs e) => ReDraw();
}