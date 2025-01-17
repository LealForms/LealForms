using LealForms.Extensions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LealForms.Controls.Forms;

/// <summary>
/// Represents a custom form with default settings and optional dark mode support.
/// </summary>
public class LealForm : Form
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LealForm"/> class.
    /// </summary>
    /// <param name="redrawOnResize">if set to <c>true</c> the form will redraw on resize.</param>
    public LealForm(bool redrawOnResize = false)
    {
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(LealConstants.DEFAULT_WIDTH, LealConstants.DEFAULT_HEIGHT);
        DoubleBuffered = true;
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

        HandleCreated += (s, e) => BeginInvoke(new Action(() =>
        {
            LoadComponents();

            if (redrawOnResize)
                Resize += (s, e) => ReDraw();
        }));
    }

    /// <summary>
    /// Redraws the form's components. Can be overridden by derived classes to implement custom redraw logic.
    /// </summary>
    public virtual void ReDraw() { }

    /// <summary>
    /// Loads custom components into the form. Can be overridden by derived classes to add custom components.
    /// </summary>
    public virtual void LoadComponents() { }
}