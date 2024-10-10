using System;
using System.Drawing;
using System.Windows.Forms;

namespace LForms.Extensions;

public static class ControlExtensions
{
    /// <summary>
    /// Hides or shows the control, toggling its visibility and enabling or disabling its functionality.
    /// </summary>
    /// <param name="control">The control to hide or show.</param>
    /// <param name="hide">Whether to show the control (true) or hide it (false).</param>
    public static void SetVisibility(this Control control, bool show = true)
    {
        control.Visible = show;
        control.Enabled = show;

        if (control is Form fm)
            fm.ShowInTaskbar = show;

        if (show)
            control.Show();
        else
            control.Hide();
    }

    /// <summary>
    /// Adds a child control to the specified control.
    /// </summary>
    /// <param name="control">The parent control.</param>
    /// <param name="childControl">The child control to add.</param>
    public static void Add(this Control control, Control childControl)
        => control.Controls.Add(childControl);

    /// <summary>
    /// Handles mouse down events and allows for window dragging, restricted to a specific mouse button.
    /// </summary>
    /// <param name="handle">Handle to the window to be dragged.</param>
    /// <param name="e">Mouse event arguments containing details about the mouse click.</param>
    /// <param name="allowedMouseButton">The mouse button that is allowed to initiate the window drag. Defaults to the left mouse button.</param>
    public static void ControlMouseDown(IntPtr handle, MouseEventArgs e, MouseButtons allowedMouseButton = MouseButtons.Left)
    {
        if (e.Button != allowedMouseButton) return;

        ExternalExtensions.ReleaseCapture();
        _ = ExternalExtensions.SendMessage(handle, Constants.WM_NCLBUTTONDOWN, Constants.HT_CAPTION, 0);
    }

    /// <summary>
    /// Handles mouse down events for window dragging, defaulting to the left mouse button.
    /// </summary>
    /// <param name="handle">Handle to the window to be dragged.</param>
    /// <param name="e">Mouse event arguments containing details about the mouse click.</param>
    public static void ControlMouseDown(IntPtr handle, MouseEventArgs e)
        => ControlMouseDown(handle, e, MouseButtons.Left);

    #region [ Positional ]
    /// <summary>
    /// Centralizes the control relative to the specified reference control.
    /// </summary>
    /// <param name="control">The control to centralize.</param>
    /// <param name="controlReference">The reference control.</param>
    public static void CentralizeRelativeTo(this Control control, Control controlReference)
    {
        HorizontalCentralize(control, controlReference);
        VerticalCentralize(control, controlReference);
    }

    /// <summary>
    /// Horizontally centralizes the control relative to the specified reference control.
    /// </summary>
    /// <param name="control">The control to centralize.</param>
    /// <param name="controlReference">The reference control.</param>
    public static void HorizontalCentralize(this Control control, Control controlReference)
    {
        var centerPoint = new Point(controlReference.Width / 2, controlReference.Height / 2);
        var xValue = centerPoint.X - (control.Width / 2);
        control.Location = new Point(xValue, control.Location.Y);
    }

    /// <summary>
    /// Vertically centralizes the control relative to the specified reference control.
    /// </summary>
    /// <param name="control">The control to centralize.</param>
    /// <param name="controlReference">The reference control.</param>
    public static void VerticalCentralize(this Control control, Control controlReference)
    {
        var centerPoint = new Point(controlReference.Width / 2, controlReference.Height / 2);
        var yValue = centerPoint.Y - (control.Height / 2);
        control.Location = new Point(control.Location.X, yValue);
    }

    /// <summary>
    /// Updates the Y-coordinate of the control's location by a specified offset.
    /// </summary>
    /// <param name="control">The control to update.</param>
    /// <param name="offset">The offset to add to the current Y-coordinate.</param>
    public static void SetY(this Control control, int offset)
    {
        var pos = control.Location;
        control.Location = new Point(pos.X, pos.Y + offset);
    }

    /// <summary>
    /// Updates the Y-coordinate of the control based on the bottom of a reference control and an offset.
    /// </summary>
    /// <param name="control">The control to update.</param>
    /// <param name="controlReference">The reference control for determining the bottom position.</param>
    /// <param name="offset">The offset from the reference control's bottom.</param>
    public static void SetYOffsetBottom(this Control control, Control controlReference, int offset)
    {
        var basePoint = new Point(controlReference.Width, controlReference.Height);
        var yValue = basePoint.Y - control.Height - offset;
        control.Location = new Point(control.Location.X, yValue);
    }

    /// <summary>
    /// Sets the Y-coordinate of the control to an absolute value.
    /// </summary>
    /// <param name="control">The control to update.</param>
    /// <param name="y">The new absolute Y-coordinate.</param>
    public static void SetYAbsolute(this Control control, int y)
    {
        var pos = control.Location;
        control.Location = new Point(pos.X, y);
    }

    /// <summary>
    /// Updates the X-coordinate of the control's location by a specified offset.
    /// </summary>
    /// <param name="control">The control to update.</param>
    /// <param name="offset">The offset to add to the current X-coordinate.</param>
    public static void SetX(this Control control, int offset)
    {
        var pos = control.Location;
        control.Location = new Point(pos.X + offset, pos.Y);
    }

    /// <summary>
    /// Updates the X-coordinate of the control based on the left of a reference control and an offset.
    /// </summary>
    /// <param name="control">The control to update.</param>
    /// <param name="controlReference">The reference control for determining the left position.</param>
    /// <param name="offset">The offset from the reference control's left.</param>
    public static void SetXFromLeft(this Control control, Control controlReference, int offset)
    {
        var basePoint = new Point(controlReference.Width, controlReference.Height);
        var xValue = basePoint.X - control.Width - offset;
        control.Location = new Point(xValue, control.Location.Y);
    }

    /// <summary>
    /// Sets the X-coordinate of the control to an absolute value.
    /// </summary>
    /// <param name="control">The control to update.</param>
    /// <param name="x">The new absolute X-coordinate.</param>
    public static void SetXAbsolute(this Control control, int x)
    {
        var pos = control.Location;
        control.Location = new Point(x, pos.Y);
    }

    /// <summary>
    /// Updates the control's X-coordinate to position it next to a reference control with a specified offset between them.
    /// </summary>
    /// <param name="control">The control to update.</param>
    /// <param name="controlReference">The reference control to align next to.</param>
    /// <param name="offsetbetween">The offset between the reference control and the updated control.</param>
    public static void SetXOffsetRight(this Control control, Control controlReference, int offsetbetween)
    {
        var newPos = controlReference.Location.X + controlReference.Width + offsetbetween;
        SetXAbsolute(control, newPos);
    }

    /// <summary>
    /// Updates the control's Y-coordinate to position it below a reference control with a specified offset between them.
    /// </summary>
    /// <param name="control">The control to update.</param>
    /// <param name="controlReference">The reference control to align below.</param>
    /// <param name="offsetbetween">The offset between the reference control and the updated control.</param>
    public static void SetYOffSetNext(this Control control, Control controlReference, int offsetbetween)
    {
        var newXPos = controlReference.Location.Y + controlReference.Height + offsetbetween;
        SetYAbsolute(control, newXPos);
    }

    /// <summary>
    /// Arranges controls of a specified type in a cascading vertical layout within their parent container.
    /// </summary>
    /// <typeparam name="T">The type of controls to arrange, derived from Control.</typeparam>
    /// <param name="collection">The collection of controls to be arranged.</param>
    /// <param name="offSet">The vertical offset between each control.</param>
    public static void WaterFallControlsOfType<T>(this Control.ControlCollection collection, int offSet) where T : Control
    {
        T? last = null;

        foreach (var control in collection)
        {
            if (control is T ctr)
            {
                ctr.CentralizeRelativeTo(ctr.Parent!);

                // If there's a "last" control defined, position the current control a certain offset below it.
                if (last != null)
                    ctr.SetYOffsetBottom(last, offSet);

                last = ctr;
            }
        }
    }
    #endregion

    #region [ Round Region ]
    public static void GenerateRoundRegion(this Control control, int curve)
        => control.Region = GenerateRoundRegion(control.Width, control.Height, curve);

    public static void GenerateRoundRegion(this Control control)
        => control.Region = GenerateRoundRegion(control.Width, control.Height);

    public static Region GenerateRoundRegion(int width, int height)
        => GenerateRoundRegion(width, height, Constants.ELIPSE_CURVE);

    public static Region GenerateRoundRegion(int width, int height, int curve)
        => Region.FromHrgn(ExternalExtensions.CreateRoundRectRgn(0, 0, width, height, curve, curve));
    #endregion

    /// <summary>
    /// Recursively searches up the control hierarchy to find the first parent that is a Form.
    /// </summary>
    /// <param name="parent">The starting control for the search.</param>
    /// <returns>The first parent Form found in the control hierarchy; null if none is found.</returns>
    public static Control? FindParentForm(this Control parent)
    {
        // Check if the current control is a Form.
        // If so, return it as the topmost parent Form has been found.
        if (parent is Form)
            return parent;

        // If the current control is not a Form but has a parent,
        // recursively call the method with the parent control to continue searching up the hierarchy.
        if (parent.Parent != null)
            return parent.Parent.FindParentForm();

        // If the current control has no parent, return null indicating no parent Form was found in the hierarchy.
        return null;
    }
}