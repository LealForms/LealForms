using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LForms.Extensions;

/// <summary>
/// Custom extension functions for <see cref="Control"/>s
/// </summary>
public static class ControlExtensions
{
    /// <summary>
    /// Hides or shows the control, toggling its visibility and enabling or disabling its functionality.
    /// </summary>
    /// <param name="control">The control to hide or show.</param>
    /// <param name="show">Whether to show the control (true) or hide it (false).</param>
    public static void ToggleVisibility(this Control control, bool show = true)
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
    public static void Add(this Control control, Control? childControl)
        => control.Controls.Add(childControl);

    /// <summary>
    /// Removes a child control from the specified control.
    /// </summary>
    /// <param name="control">The parent control.</param>
    /// <param name="childControl">The child control to remove.</param>
    public static void Remove(this Control control, Control? childControl)
        => control.Controls.Remove(childControl);

    /// <summary>
    /// Handles mouse down events and allows for window dragging, restricted to a specific mouse button.
    /// </summary>
    /// <param name="handle">Handle to the window to be dragged.</param>
    /// <param name="e">Mouse event arguments containing details about the mouse click.</param>
    /// <param name="allowedMouseButton">The mouse button that is allowed to initiate the window drag. Defaults to the left mouse button.</param>
    public static void DragWindowOnMouseDown(this IntPtr handle, MouseEventArgs e, MouseButtons allowedMouseButton = MouseButtons.Left)
    {
        if (e.Button != allowedMouseButton) return;

        ExternalExtensions.ReleaseCapture();
        _ = ExternalExtensions.SendMessage(handle, LealConstants.WM_NCLBUTTONDOWN, LealConstants.HT_CAPTION, 0);
    }

    /// <summary>
    /// Handles mouse down events for window dragging, defaulting to the left mouse button.
    /// </summary>
    /// <param name="form">form that will be dragged.</param>
    /// <param name="e">Mouse event arguments containing details about the mouse click.</param>
    public static void DragWindowOnMouseDown(this Form form, MouseEventArgs e)
        => form.Handle.DragWindowOnMouseDown(e);

    /// <summary>
    /// Handles mouse down events for window dragging, defaulting to the left mouse button.
    /// </summary>
    /// <param name="handle">Handle to the window to be dragged.</param>
    /// <param name="e">Mouse event arguments containing details about the mouse click.</param>
    public static void DragWindowOnMouseDown(this IntPtr handle, MouseEventArgs e)
        => handle.DragWindowOnMouseDown(e, MouseButtons.Left);

    #region [ Positional ]
    /// <summary>
    /// Centralizes the control relative to the parent.
    /// </summary>
    /// <param name="control">The control to centralize.</param>
    public static void Centralize(this Control control)
    {
        if (control.Parent == null) return;

        CentralizeRelativeTo(control, control.Parent);
    }

    /// <summary>
    /// Centralizes the control relative to the specified reference control.
    /// </summary>
    /// <param name="control">The control to centralize.</param>
    /// <param name="controlReference">The reference control.</param>
    public static void CentralizeRelativeTo(this Control control, Control controlReference)
    {
        control.HorizontalCentralize(controlReference);
        control.VerticalCentralize(controlReference);
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
        control.SetX(xValue);
    }

    /// <summary>
    /// Horizontally centralizes the control relative to the parent.
    /// </summary>
    /// <param name="control">The control to centralize.</param>
    public static void HorizontalCentralize(this Control control)
    {
        if (control.Parent == null) return;

        control.HorizontalCentralize(control.Parent);
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
        control.SetY(yValue);
    }

    /// <summary>
    /// Vertically centralizes the control relative to the parent.
    /// </summary>
    /// <param name="control">The control to centralize.</param>
    public static void VerticalCentralize(this Control control)
    {
        if (control.Parent == null) return;

        control.VerticalCentralize(control.Parent);
    }

    /// <summary>
    /// Sets the X coordinate of the control's location.
    /// </summary>
    /// <param name="control">The control to modify.</param>
    /// <param name="x">The new X coordinate.</param>
    public static void SetX(this Control control, int x)
        => control.Location = new Point(x, control.Location.Y);

    /// <summary>
    /// Sets the Y coordinate of the control's location.
    /// </summary>
    /// <param name="control">The control to modify.</param>
    /// <param name="y">The new Y coordinate.</param>
    public static void SetY(this Control control, int y)
        => control.Location = new Point(control.Location.X, y);

    /// <summary>
    /// Increases the X coordinate of the control's location by a specified offset.
    /// </summary>
    /// <param name="control">The control to modify.</param>
    /// <param name="offset">The value to add to the X coordinate.</param>
    public static void AddX(this Control control, int offset)
        => control.SetX(control.Location.X + offset);

    /// <summary>
    /// Increases the Y coordinate of the control's location by a specified offset.
    /// </summary>
    /// <param name="control">The control to modify.</param>
    /// <param name="offset">The value to add to the Y coordinate.</param>
    public static void AddY(this Control control, int offset)
        => control.SetY(control.Location.Y + offset);

    /// <summary>
    /// Positions the control vertically above a reference control, with an optional offset.
    /// </summary>
    /// <param name="control">The control to position.</param>
    /// <param name="controlReference">The reference control to position relative to.</param>
    /// <param name="offset">The distance to offset the control from the reference control.</param>
    public static void SetYBeforeControl(this Control control, Control controlReference, int offset)
        => control.SetY(controlReference.Location.Y - control.Height - offset);

    /// <summary>
    /// Positions the control vertically below a reference control, with an optional offset.
    /// </summary>
    /// <param name="control">The control to position.</param>
    /// <param name="controlReference">The reference control to position relative to.</param>
    /// <param name="offset">The distance to offset the control from the reference control.</param>
    public static void SetYAfterControl(this Control control, Control controlReference, int offset)
        => control.SetY(controlReference.Location.Y + controlReference.Height + offset);

    /// <summary>
    /// Positions the control horizontally before a reference control, with an optional offset.
    /// </summary>
    /// <param name="control">The control to position.</param>
    /// <param name="controlReference">The reference control to position relative to.</param>
    /// <param name="offset">The distance to offset the control from the reference control.</param>
    public static void SetXBeforeControl(this Control control, Control controlReference, int offset)
        => control.SetX(controlReference.Location.X - control.Width - offset);

    /// <summary>
    /// Positions the control horizontally after a reference control, with an optional offset.
    /// </summary>
    /// <param name="control">The control to position.</param>
    /// <param name="controlReference">The reference control to position relative to.</param>
    /// <param name="offset">The distance to offset the control from the reference control.</param>
    public static void SetXAfterControl(this Control control, Control controlReference, int offset)
        => control.SetX(controlReference.Location.X + controlReference.Width + offset);

    /// <summary>
    /// Positions all child controls of the specified type <typeparamref name="T"/> vertically in a waterfall layout.
    /// The first control starts at the given initial Y coordinate, and subsequent controls are spaced by the specified offset.
    /// </summary>
    /// <typeparam name="T">The type of controls to arrange.</typeparam>
    /// <param name="control">The parent control containing child controls.</param>
    /// <param name="inicialY">The Y coordinate to start positioning the first control.</param>
    /// <param name="offSetBetween">The vertical offset between each consecutive control of type <typeparamref name="T"/>.</param>
    public static void WaterFallChildControlsOfTypeByY<T>(this Control control, int inicialY, int offSetBetween) where T : Control
    {
        T? last = null;

        foreach (var child in control.Controls)
        {
            if (child is not T ctr)
                continue;

            if (last != null)
                ctr.SetYAfterControl(last, offSetBetween);
            else 
                ctr.SetY(inicialY);

            last = ctr;
        }
    }

    /// <summary>
    /// Positions all child controls of the specified type <typeparamref name="T"/> horizontally in a waterfall layout.
    /// The first control starts at the given initial X coordinate, and subsequent controls are spaced by the specified offset.
    /// </summary>
    /// <typeparam name="T">The type of controls to arrange.</typeparam>
    /// <param name="control">The parent control containing child controls.</param>
    /// <param name="initialX">The X coordinate to start positioning the first control.</param>
    /// <param name="offSetBetween">The horizontal offset between each consecutive control of type <typeparamref name="T"/>.</param>
    public static void WaterFallChildControlsOfTypeByX<T>(this Control control, int initialX, int offSetBetween) where T : Control
    {
        T? last = null;

        foreach (var child in control.Controls)
        {
            if (child is not T ctr)
                continue;

            if (last != null)
                ctr.SetXAfterControl(last, offSetBetween);
            else
                ctr.SetX(initialX);

            last = ctr;
        }
    }
    #endregion

    #region [ Round Region ]
    /// <summary>
    /// Sets a rounded rectangular region for the specified <see cref="Control"/> with a custom curve radius.
    /// </summary>
    /// <param name="control">The control to apply the rounded region to.</param>
    /// <param name="curve">The radius of the curve for the rounded corners.</param>
    public static void GenerateRoundRegion(this Control control, int curve)
        => control.Region = GenerateRoundRegion(control.Width, control.Height, curve);

    /// <summary>
    /// Sets a default rounded rectangular region for the specified <see cref="Control"/>.
    /// </summary>
    /// <param name="control">The control to apply the rounded region to.</param>
    public static void GenerateRoundRegion(this Control control)
        => control.Region = GenerateRoundRegion(control.Width, control.Height);

    /// <summary>
    /// Creates a default rounded rectangular <see cref="Region"/> with pre-defined curve radius.
    /// </summary>
    /// <param name="width">The width of the region.</param>
    /// <param name="height">The height of the region.</param>
    /// <returns>A <see cref="Region"/> with rounded corners.</returns>
    public static Region GenerateRoundRegion(int width, int height)
        => GenerateRoundRegion(width, height, LealConstants.ELIPSE_CURVE);

    /// <summary>
    /// Creates a rounded rectangular <see cref="Region"/> based on specified width, height, and curve radius.
    /// </summary>
    /// <param name="width">The width of the region.</param>
    /// <param name="height">The height of the region.</param>
    /// <param name="curve">The radius of the curve for the rounded corners.</param>
    /// <returns>A <see cref="Region"/> with rounded corners.</returns>
    public static Region GenerateRoundRegion(int width, int height, int curve)
        => Region.FromHrgn(ExternalExtensions.CreateRoundRectRgn(0, 0, width, height, curve, curve));

    /// <summary>
    /// Sets a rounded rectangular region for the specified <see cref="Control"/> with custom corner options.
    /// </summary>
    /// <param name="control">The control to apply the rounded region to.</param>
    /// <param name="curve">The radius of the curve for the rounded corners.</param>
    /// <param name="topLeft">Whether to round the top-left corner.</param>
    /// <param name="topRight">Whether to round the top-right corner.</param>
    /// <param name="bottomLeft">Whether to round the bottom-left corner.</param>
    /// <param name="bottomRight">Whether to round the bottom-right corner.</param>
    public static void GenerateCustomRoundRegion(this Control control, int curve, bool topLeft, bool topRight, bool bottomLeft, bool bottomRight)
        => control.Region = GenerateCustomRoundRegion(control.Width, control.Height, curve, topLeft, topRight, bottomLeft, bottomRight);

    /// <summary>
    /// Creates a rounded rectangular <see cref="Region"/> based on specified width, height, curve radius, and corner options.
    /// </summary>
    /// <param name="width">The width of the region.</param>
    /// <param name="height">The height of the region.</param>
    /// <param name="radius">The radius of the curve for the rounded corners.</param>
    /// <param name="topLeft">Whether to round the top-left corner.</param>
    /// <param name="topRight">Whether to round the top-right corner.</param>
    /// <param name="bottomLeft">Whether to round the bottom-left corner.</param>
    /// <param name="bottomRight">Whether to round the bottom-right corner.</param>
    /// <returns>A <see cref="Region"/> with specified rounded corners.</returns>
    public static Region GenerateCustomRoundRegion(int width, int height, int radius, bool topLeft, bool topRight, bool bottomLeft, bool bottomRight)
    {
        var path = new GraphicsPath();
        int arcWidth = radius * 2;
        int arcHeight = radius * 2;

        // Start at the top-left corner
        if (topLeft)
            path.AddArc(0, 0, arcWidth, arcHeight, 180, 90);
        else
            path.AddLine(0, 0, 0, 0);

        // Top edge
        path.AddLine(topLeft ? radius : 0, 0, width - (topRight ? radius : 0), 0);

        // Top-right corner
        if (topRight)
            path.AddArc(width - arcWidth, 0, arcWidth, arcHeight, 270, 90);
        else
            path.AddLine(width, 0, width, 0);

        // Right edge
        path.AddLine(width, topRight ? radius : 0, width, height - (bottomRight ? radius : 0));

        // Bottom-right corner
        if (bottomRight)
            path.AddArc(width - arcWidth, height - arcHeight, arcWidth, arcHeight, 0, 90);
        else
            path.AddLine(width, height, width, height);

        // Bottom edge
        path.AddLine(width - (bottomRight ? radius : 0), height, (bottomLeft ? radius : 0), height);

        // Bottom-left corner
        if (bottomLeft)
            path.AddArc(0, height - arcHeight, arcWidth, arcHeight, 90, 90);
        else
            path.AddLine(0, height, 0, height);

        // Left edge
        path.AddLine(0, height - (bottomLeft ? radius : 0), 0, topLeft ? radius : 0);

        path.CloseFigure();

        return new Region(path);
    }
    #endregion

    /// <summary>
    /// Recursively searches for and returns the closest parent control of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the parent control to search for, which must inherit from <see cref="Control"/>.</typeparam>
    /// <param name="control">The control from which to begin the search.</param>
    /// <returns>The closest parent control of type <typeparamref name="T"/> if found; otherwise, <c>null</c>.</returns>
    public static T? GetClosestParentOfType<T>(this Control control) where T : Control
    {
        if (control is T target)
            return target;

        if (control.Parent != null)
            return control.Parent.GetClosestParentOfType<T>();

        return null;
    }

    /// <summary>
    /// Retrieves all child controls of the specified type from the given <see cref="Control.ControlCollection"/>.
    /// </summary>
    /// <typeparam name="T">The type of control to search for, which must inherit from <see cref="Control"/>.</typeparam>
    /// <param name="collection">The collection of controls to search through.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all child controls of the specified type.</returns>
    public static IEnumerable<T> GetChildsOfType<T>(this Control.ControlCollection collection) where T : Control
    {
        foreach (var control in collection)
            if (control is T target)
                yield return target;
    }

    /// <summary>
    /// Retrieves all child controls of the specified type from the given <see cref="Control"/>.
    /// </summary>
    /// <typeparam name="T">The type of control to search for, which must inherit from <see cref="Control"/>.</typeparam>
    /// <param name="control">The control whose children are to be searched.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all child controls of the specified type.</returns>
    public static IEnumerable<T> GetChildsOfType<T>(this Control control) where T : Control
        => GetChildsOfType<T>(control.Controls);

    /// <summary>
    /// Determines the resize direction based on the mouse location relative to the edges
    /// and corners of the specified control. Returns 0 if no resizing should be triggered.
    /// </summary>
    /// <param name="control">The control to check the mouse position against.</param>
    /// <param name="location">The current mouse position relative to the control.</param>
    /// <returns>An integer representing the resize direction, or 0 if no resizing is triggered.</returns>
    public static int GetResizeDirection(this Control control, Point location)
    {
        var inLeft = location.X <= LealConstants.C_GRIP;
        var inRight = location.X >= control.Width - LealConstants.C_GRIP;
        var inBottom = location.Y >= control.Height - LealConstants.C_GRIP;

        if (inRight && inBottom)
            return LealConstants.WMSZ_BOTTOMRIGHT;

        if (inRight)
            return LealConstants.WMSZ_RIGHT;

        if (inLeft && inBottom)
            return LealConstants.WMSZ_BOTTOMLEFT;

        if (inLeft)
            return LealConstants.WMSZ_LEFT;

        if (inBottom)
            return LealConstants.WMSZ_BOTTOM;

        return 0;
    }
}