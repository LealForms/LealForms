using LForms.Extensions;
using System;
using System.Windows.Forms;

namespace LForms.Controls.Panels;

/// <summary>
/// Basic implementation for a panel, enables double buffering
/// </summary>
public class LealPanel : Panel
{
    /// <summary>
    /// Initialize a new instance of <see cref="LealPanel"/>
    /// </summary>
    /// <param name="isPanelDragged">set up event handler for dragging the parent form functionality.</param>
    /// <param name="redrawOnResize">set up event handler for redrawing the panel on resize.</param>
    public LealPanel(bool isPanelDragged = false, bool redrawOnResize = false)
    {
        DoubleBuffered = true;
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

        // Load custom components in the form asynchronously after the panel is created
        HandleCreated += (s, e) => BeginInvoke(new Action(() =>
        {
            LoadComponents();

            if (redrawOnResize)
                Resize += (s, e) => ReDraw();

            if (isPanelDragged)
                MouseDown += LealPanel_MouseDown;
        }));
    }

    /// <summary>
    /// Handles the MouseDown event to initiate dragging of the panel.
    /// </summary>
    private void LealPanel_MouseDown(object? sender, MouseEventArgs e)
        => this.GetClosestParentOfType<Form>()?.Handle.DragWindowOnMouseDown(e);

    /// <summary>
    /// Forces a redraw of the panel, optionally implemented by derived classes.
    /// </summary>
    protected virtual void ReDraw() { }

    /// <summary>
    /// Loads custom components into the panel. optionally implemented by derived classes.
    /// </summary>
    protected virtual void LoadComponents() { }
}