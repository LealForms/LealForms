using LForms.Extensions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LForms.Controls.Forms;

/// <summary>
/// Represents a custom form with default settings and optional dark mode support.
/// </summary>
public class LealForm : Form
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LealForm"/> class.
    /// Sets default size, position, and applies dark mode if available.
    /// </summary>
    public LealForm()
    {
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(LealConstants.DEFAULT_WIDTH, LealConstants.DEFAULT_HEIGHT);
        DoubleBuffered = true;
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        this.TrySetDarkMode();
        Resize += OnResize;
        Load += (s, e) => LoadComponents();
    }

    /// <summary>
    /// Attempts to apply dark mode to the top bar of this form, based on your operational system theme.
    /// </summary>
    /// <returns><c>true</c> if dark mode was applied successfully; otherwise, <c>false</c>.</returns>
    public bool TryDarkMode() => this.TrySetDarkMode();

    /// <summary>
    /// Loads custom components into the form. Can be overridden by derived classes to add custom components.
    /// </summary>
    public virtual void LoadComponents() { }

    /// <summary>
    /// Redraws the form's components. Can be overridden by derived classes to implement custom redraw logic.
    /// </summary>
    public virtual void ReDraw() { }

    /// <summary>
    /// Sets the form's minimum and maximum size to the specified fixed dimensions,
    /// preventing the form from being resized.
    /// </summary>
    /// <param name="width">The fixed width for the form.</param>
    /// <param name="height">The fixed height for the form.</param>
    public void SetFixedSize(int width, int height)
    {
        MinimumSize = new Size(width, height);
        MaximumSize = new Size(width, height);
    }

    /// <summary>
    /// Frees the fixed size constraints on the form, allowing it to be resized 
    /// freely by resetting the minimum and maximum size to their respective limits.
    /// </summary>
    public void FreeUpFixedSize()
    {
        MinimumSize = new Size(int.MinValue, int.MinValue);
        MaximumSize = new Size(int.MaxValue, int.MaxValue);
    }

    /// <summary>
    /// Handles the Resize event of the form.
    /// </summary>
    private void OnResize(object? sender, EventArgs e) => ReDraw();

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
            Resize -= OnResize;

        base.Dispose(disposing);
    }
}