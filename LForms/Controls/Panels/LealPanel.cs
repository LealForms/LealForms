using LForms.Extensions;
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
    public LealPanel(bool isPanelDragged = false)
    {
        DoubleBuffered = true;
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        LoadComponents();

        if (isPanelDragged)
            MouseDown += LealPanel_MouseDown;
    }

    /// <summary>
    /// Handles the MouseDown event to initiate dragging of the panel.
    /// </summary>
    private void LealPanel_MouseDown(object? sender, MouseEventArgs e)
        => this.GetClosestParentOfType<Form>()?.Handle.DragWindowOnMouseDown(e);

    /// <summary>
    /// Forces a redraw of the button, optionally implemented by derived classes.
    /// </summary>
    protected virtual void ReDraw() { }

    /// <summary>
    /// Loads custom components into the form. Can be overridden by derived classes to add custom components.
    /// </summary>
    protected virtual void LoadComponents() { }
}