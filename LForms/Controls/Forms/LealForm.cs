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
        this.TrySetDarkMode();
        LoadComponents();
        Resize += OnResize;
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