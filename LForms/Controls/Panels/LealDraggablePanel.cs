using LForms.Extensions;
using System.Windows.Forms;

namespace LForms.Controls.Panels;

/// <summary>
/// Basic implementation for a draggable panel
/// </summary>
public class LealDraggablePanel : LealPanel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LealDraggablePanel"/> class,
    /// setting up event handlers for dragging functionality.
    /// </summary>
    public LealDraggablePanel()
    {
        MouseDown += LealDraggablePanel_MouseDown;
    }

    /// <summary>
    /// Handles the MouseDown event to initiate dragging of the panel.
    /// </summary>
    private void LealDraggablePanel_MouseDown(object? sender, MouseEventArgs e)
        => this.GetClosestParentOfType<Form>()?.Handle.DragWindowOnMouseDown(e);
}