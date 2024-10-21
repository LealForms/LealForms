using LForms.Extensions;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LForms.Controls.Panels;

/// <summary>
/// A panel that supports resizing from various edges and corners. 
/// The resizing behavior is triggered by mouse actions and 
/// integrates with form resizing commands using custom constants.
/// </summary>
public class LealResizablePanel : LealPanel
{
    /// <summary>
    /// Mapping of WMSZ constants to their corresponding Cursors.
    /// Used to change the cursor based on the resize direction.
    /// </summary>
    private readonly Dictionary<int, Cursor> _WindowMessageToCursor = new()
    {
        { 0, Cursors.Default }, // No resizing
        { LealConstants.WMSZ_LEFT, Cursors.SizeWE },         // Left edge resizing (horizontal)
        { LealConstants.WMSZ_RIGHT, Cursors.SizeWE },        // Right edge resizing (horizontal)
        { LealConstants.WMSZ_TOP, Cursors.SizeNS },          // Top edge resizing (vertical)
        { LealConstants.WMSZ_BOTTOM, Cursors.SizeNS },       // Bottom edge resizing (vertical)
        { LealConstants.WMSZ_TOPLEFT, Cursors.SizeNWSE },    // Top-left corner resizing (diagonal)
        { LealConstants.WMSZ_TOPRIGHT, Cursors.SizeNESW },   // Top-right corner resizing (diagonal)
        { LealConstants.WMSZ_BOTTOMLEFT, Cursors.SizeNESW }, // Bottom-left corner resizing (diagonal)
        { LealConstants.WMSZ_BOTTOMRIGHT, Cursors.SizeNWSE } // Bottom-right corner resizing (diagonal)
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="LealResizablePanel"/> class.
    /// </summary>
    public LealResizablePanel() { }

    /// <summary>
    /// Handles mouse down events and starts the resizing process
    /// if the mouse is near the edges or corners of the panel.
    /// </summary>
    /// <param name="e">The mouse event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
        var direction = this.GetResizeDirection(e.Location);
        var parent = this.GetClosestParentOfType<Form>();

        if (direction == 0 || parent == null)
        {
            base.OnMouseDown(e);
            return;
        }

        var resizeMessage = LealConstants.SC_SIZE + direction;
        ExternalExtensions.ReleaseCapture();
        _ = ExternalExtensions.SendMessage(parent.Handle, LealConstants.WM_SYSCOMMAND, resizeMessage, 0);
    }

    /// <summary>
    /// Handles mouse move events to update the cursor based on the 
    /// position of the mouse relative to the panel's edges and corners.
    /// </summary>
    /// <param name="e">The mouse event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
        var direction = this.GetResizeDirection(e.Location);
        Cursor = _WindowMessageToCursor[direction];
        base.OnMouseMove(e);
    }
}