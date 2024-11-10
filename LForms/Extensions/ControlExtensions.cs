using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace LForms.Extensions;

/// <summary>
/// Custom extension functions for <see cref="Control"/>s
/// </summary>
public static class ControlExtensions
{
    #region [ Visibility ]
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
    #endregion

    #region [ Add/Remove Controls ]
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
    #endregion

    #region [ Window Dragging ]
    /// <summary>
    /// Handles mouse down events and allows for window dragging, restricted to a specific mouse button.
    /// </summary>
    /// <param name="handle">Handle to the window to be dragged.</param>
    /// <param name="e">Mouse event arguments containing details about the mouse click.</param>
    /// <param name="allowedMouseButton">The mouse button that is allowed to initiate the window drag. Defaults to the left mouse button.</param>
    public static void DragWindowOnMouseDown(this IntPtr handle, MouseEventArgs e, MouseButtons allowedMouseButton)
    {
        if (e.Button != allowedMouseButton) return;

        ExternalExtensions.ReleaseCapture();
        _ = ExternalExtensions.SendMessage(handle, LealConstants.WM_NCLBUTTONDOWN, LealConstants.HT_CAPTION, 0);
    }

    /// <summary>
    /// Handles mouse down events for window dragging, defaulting to the left mouse button.
    /// </summary>
    /// <param name="handle">Handle to the window to be dragged.</param>
    /// <param name="e">Mouse event arguments containing details about the mouse click.</param>
    public static void DragWindowOnMouseDown(this IntPtr handle, MouseEventArgs e)
        => handle.DragWindowOnMouseDown(e, MouseButtons.Left);
    
    /// <summary>
    /// Adds a mouse down event to a control that allows its parent form to be dragged when the control is clicked.
    /// </summary>
    /// <param name="control">The control whose mouse down event will initiate form dragging.</param>
    public static void DragWindowOnMouseDown(this Control control)
    {
        var form = control.FindForm();

        if (form == null) 
            return;

        control.MouseDown += (sender, e) => form.Handle.DragWindowOnMouseDown(e);
    }
    #endregion

    #region [ DockingExtensions ]
    /// <summary>
    /// Docks the control to fill its parent container, applying individual padding values for each side.
    /// Padding values of 0 indicate no change to the respective side.
    /// </summary>
    /// <param name="control">The control to be docked within its parent container.</param>
    /// <param name="leftPadding">Padding value for the left side (in pixels).</param>
    /// <param name="rightPadding">Padding value for the right side (in pixels).</param>
    /// <param name="bottomPadding">Padding value for the bottom side (in pixels).</param>
    /// <param name="topPadding">Padding value for the top side (in pixels).</param>
    /// /// <remarks>
    /// This method sets the control's <see cref="Control.Anchor"/> property to ensure it resizes with its parent,
    /// and positions it based on the specified padding values.
    /// </remarks>
    public static void DockFillWithPadding(this Control control, int leftPadding, int rightPadding, int bottomPadding, int topPadding)
    {
        var parent = control.Parent;

        if (parent == null)
            return;

        control.Width = parent.Width - leftPadding - rightPadding;
        control.Height = parent.Height - topPadding - bottomPadding;

        control.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        control.SetX(leftPadding);
        control.SetY(topPadding);
    }
    
    /// <summary>
    /// Docks the specified control to fill its parent container, applying uniform padding on all sides.
    /// </summary>
    /// <param name="control">The control to be docked within its parent container.</param>
    /// <param name="padding">The padding value for all sides, in pixels.</param>
    /// <remarks>
    /// This overload applies the same padding to the left, right, top, and bottom of the control.
    /// </remarks>
    public static void DockFillWithPadding(this Control control, int padding)
        => control.DockFillWithPadding(padding, padding, padding, padding);

    /// <summary>
    /// Docks the control to the left side of its parent container, applying the specified padding.
    /// </summary>
    /// <param name="control">The control to be docked within its parent container.</param>
    /// <param name="leftPadding">Padding value for the left side (in pixels).</param>
    public static void DockLeftWithPadding(this Control control, int leftPadding)
    {
        var parent = control.Parent;

        if (parent == null)
            return;

        control.Anchor = AnchorStyles.Left;
        control.SetX(leftPadding);
    }

    /// <summary>
    /// Docks the control to the right side of its parent container, applying the specified padding.
    /// </summary>
    /// <param name="control">The control to be docked within its parent container.</param>
    /// <param name="rightPadding">Padding value for the right side (in pixels).</param>
    public static void DockRightWithPadding(this Control control, int rightPadding)
    {
        var parent = control.Parent;

        if (parent == null)
            return;

        control.Anchor = AnchorStyles.Right;
        control.SetX(parent.Width - control.Width - rightPadding);
    }

    /// <summary>
    /// Docks the control to the top of its parent container, applying the specified padding.
    /// </summary>
    /// <param name="control">The control to be docked within its parent container.</param>
    /// <param name="topPadding">Padding value for the top side (in pixels).</param>
    public static void DockTopWithPadding(this Control control, int topPadding)
    {
        var parent = control.Parent;

        if (parent == null)
            return;

        control.Anchor = AnchorStyles.Top;
        control.SetY(topPadding);
    }

    /// <summary>
    /// Docks the control to the bottom of its parent container, applying the specified padding.
    /// </summary>
    /// <param name="control">The control to be docked within its parent container.</param>
    /// <param name="bottomPadding">Padding value for the bottom side (in pixels).</param>
    public static void DockBottomWithPadding(this Control control, int bottomPadding)
    {
        var parent = control.Parent;

        if (parent == null)
            return;

        control.Anchor = AnchorStyles.Bottom;
        control.SetY(parent.Height - control.Height - bottomPadding);
    }

    /// <summary>
    /// Docks the control to both the top and bottom edges of its parent container, applying the specified padding.
    /// </summary>
    /// <param name="control">The control to be docked within its parent container.</param>
    /// <param name="topPadding">Padding value for the top side (in pixels).</param>
    /// <param name="bottomPadding">Padding value for the bottom side (in pixels).</param>
    public static void DockTopBottomWithPadding(this Control control, int topPadding, int bottomPadding)
    {
        var parent = control.Parent;

        if (parent == null)
            return;

        control.Height = parent.Height - topPadding - bottomPadding;
        control.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
        control.SetY(topPadding);
    }

    /// <summary>
    /// Docks the control to the top-left corner of its parent container, applying the specified padding.
    /// </summary>
    /// <param name="control">The control to be docked within its parent container.</param>
    /// <param name="topPadding">Padding value for the top side (in pixels).</param>
    /// <param name="leftPadding">Padding value for the left side (in pixels).</param>
    public static void DockTopLeftWithPadding(this Control control, int topPadding, int leftPadding)
    {
        var parent = control.Parent;

        if (parent == null)
            return;

        control.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        control.SetX(leftPadding);
        control.SetY(topPadding);
    }

    /// <summary>
    /// Docks the control to the top-right corner of its parent container, applying the specified padding.
    /// </summary>
    /// <param name="control">The control to be docked within its parent container.</param>
    /// <param name="topPadding">Padding value for the top side (in pixels).</param>
    /// <param name="rightPadding">Padding value for the right side (in pixels).</param>
    public static void DockTopRightWithPadding(this Control control, int topPadding, int rightPadding)
    {
        var parent = control.Parent;

        if (parent == null)
            return;

        control.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        control.SetX(parent.Width - control.Width - rightPadding);
        control.SetY(topPadding);
    }

    /// <summary>
    /// Docks the control to the bottom-left corner of its parent container, applying the specified padding.
    /// </summary>
    /// <param name="control">The control to be docked within its parent container.</param>
    /// <param name="bottomPadding">Padding value for the bottom side (in pixels).</param>
    /// <param name="leftPadding">Padding value for the left side (in pixels).</param>
    public static void DockBottomLeftWithPadding(this Control control, int bottomPadding, int leftPadding)
    {
        var parent = control.Parent;

        if (parent == null)
            return;

        control.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        control.SetX(leftPadding);
        control.SetY(parent.Height - control.Height - bottomPadding);
    }

    /// <summary>
    /// Docks the control to the bottom-right corner of its parent container, applying the specified padding.
    /// </summary>
    /// <param name="control">The control to be docked within its parent container.</param>
    /// <param name="bottomPadding">Padding value for the bottom side (in pixels).</param>
    /// <param name="rightPadding">Padding value for the right side (in pixels).</param>
    public static void DockBottomRightWithPadding(this Control control, int bottomPadding, int rightPadding)
    {
        var parent = control.Parent;

        if (parent == null)
            return;

        control.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        control.SetX(parent.Width - control.Width - rightPadding);
        control.SetY(parent.Height - control.Height - bottomPadding);
    }

    /// <summary>
    /// Docks the control to both the left and right edges of its parent container, applying the specified padding.
    /// </summary>
    /// <param name="control">The control to be docked within its parent container.</param>
    /// <param name="leftPadding">Padding value for the left side (in pixels).</param>
    /// <param name="rightPadding">Padding value for the right side (in pixels).</param>
    public static void DockLeftRightWithPadding(this Control control, int leftPadding, int rightPadding)
    {
        var parent = control.Parent;

        if (parent == null)
            return;

        control.Width = parent.Width - leftPadding - rightPadding;
        control.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        control.SetX(leftPadding);
    }

    /// <summary>
    /// Docks the control to the top, bottom, and left edges of its parent container, applying the specified padding.
    /// </summary>
    /// <param name="control">The control to be docked within its parent container.</param>
    /// <param name="topPadding">Padding value for the top side (in pixels).</param>
    /// <param name="bottomPadding">Padding value for the bottom side (in pixels).</param>
    /// <param name="leftPadding">Padding value for the left side (in pixels).</param>
    public static void DockTopBottomLeftWithPadding(this Control control, int topPadding, int bottomPadding, int leftPadding)
    {
        var parent = control.Parent;

        if (parent == null)
            return;

        control.Height = parent.Height - topPadding - bottomPadding;
        control.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        control.SetX(leftPadding);
        control.SetY(topPadding);
    }

    /// <summary>
    /// Docks the control to the top, bottom, and right edges of its parent container, applying the specified padding.
    /// </summary>
    /// <param name="control">The control to be docked within its parent container.</param>
    /// <param name="topPadding">Padding value for the top side (in pixels).</param>
    /// <param name="bottomPadding">Padding value for the bottom side (in pixels).</param>
    /// <param name="rightPadding">Padding value for the right side (in pixels).</param>
    public static void DockTopBottomRightWithPadding(this Control control, int topPadding, int bottomPadding, int rightPadding)
    {
        var parent = control.Parent;

        if (parent == null)
            return;

        control.Height = parent.Height - topPadding - bottomPadding;
        control.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        control.SetX(parent.Width - control.Width - rightPadding);
        control.SetY(topPadding);
    }

    /// <summary>
    /// Docks the control to the top, left, and right edges of its parent container, applying the specified padding.
    /// </summary>
    /// <param name="control">The control to be docked within its parent container.</param>
    /// <param name="topPadding">Padding value for the top side (in pixels).</param>
    /// <param name="leftPadding">Padding value for the left side (in pixels).</param>
    /// <param name="rightPadding">Padding value for the right side (in pixels).</param>
    public static void DockTopLeftRightWithPadding(this Control control, int topPadding, int leftPadding, int rightPadding)
    {
        var parent = control.Parent;

        if (parent == null)
            return;

        control.Width = parent.Width - leftPadding - rightPadding;
        control.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        control.SetX(leftPadding);
        control.SetY(topPadding);
    }

    /// <summary>
    /// Docks the control to the bottom, left, and right edges of its parent container, applying the specified padding.
    /// </summary>
    /// <param name="control">The control to be docked within its parent container.</param>
    /// <param name="bottomPadding">Padding value for the bottom side (in pixels).</param>
    /// <param name="leftPadding">Padding value for the left side (in pixels).</param>
    /// <param name="rightPadding">Padding value for the right side (in pixels).</param>
    public static void DockBottomLeftRightWithPadding(this Control control, int bottomPadding, int leftPadding, int rightPadding)
    {
        var parent = control.Parent;

        if (parent == null)
            return;

        control.Width = parent.Width - leftPadding - rightPadding;
        control.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        control.SetX(leftPadding);
        control.SetY(parent.Height - control.Height - bottomPadding);
    }

    /// <summary>
    /// Docks the control to all edges of its parent container, applying the specified padding.
    /// </summary>
    /// <param name="control">The control to be docked within its parent container.</param>
    /// <param name="topPadding">Padding value for the top side (in pixels).</param>
    /// <param name="bottomPadding">Padding value for the bottom side (in pixels).</param>
    /// <param name="leftPadding">Padding value for the left side (in pixels).</param>
    /// <param name="rightPadding">Padding value for the right side (in pixels).</param>
    public static void DockTopBottomLeftRightWithPadding(this Control control, int topPadding, int bottomPadding, int leftPadding, int rightPadding)
    {
        var parent = control.Parent;

        if (parent == null)
            return;

        control.Width = parent.Width - leftPadding - rightPadding;
        control.Height = parent.Height - topPadding - bottomPadding;
        control.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        control.SetX(leftPadding);
        control.SetY(topPadding);
    }
    #endregion

    #region [ Positional ]
    /// <summary>
    /// Centralizes the control relative to the parent.
    /// </summary>
    /// <param name="control">The control to centralize.</param>
    public static void Centralize(this Control control)
    {
        if (control.Parent == null) return;

        Centralize(control, control.Parent);
    }

    /// <summary>
    /// Centralizes the control relative to the specified reference control.
    /// </summary>
    /// <param name="control">The control to centralize.</param>
    /// <param name="controlReference">The reference control.</param>
    public static void Centralize(this Control control, Control controlReference)
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
        var clientArea = controlReference.ClientSize;
        var centerPoint = new Point(clientArea.Width / 2, clientArea.Height / 2);
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
    /// Offsets the position of the control by the specified amounts in the X and Y directions.
    /// </summary>
    /// <param name="control">The control to offset.</param>
    /// <param name="x">The amount to offset the control in the X direction.</param>
    /// <param name="y">The amount to offset the control in the Y direction.</param>
    public static void OffsetPosition(this Control control, int x, int y)
    {
        control.SetX(control.Location.X + x);
        control.SetY(control.Location.Y + y);
    }

    /// <summary>
    /// Offsets the position of the control by the specified offset.
    /// </summary>
    /// <param name="control">The control to offset.</param>
    /// <param name="offset">The amount to offset the control in the offset direction.</param>
    public static void OffsetPosition(this Control control, Point offset)
        => control.OffsetPosition(offset.X, offset.Y);

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
    /// Sets the vertical position of the control relative to its parent's bottom border.
    /// </summary>
    /// <param name="control">The control whose position is being set.</param>
    /// <param name="distanceToBottom">The distance, in pixels, from the bottom border of the parent control.</param>
    /// <remarks>
    /// This method positions the control by calculating the new Y-coordinate as the difference between the parent's height, 
    /// the control's height, and the specified distance from the bottom border.
    /// </remarks>
    public static void SetYFromParentBottom(this Control control, int distanceToBottom)
    {
        if (control.Parent == null) return;

        control.SetY(control.Parent.Height - control.Height - distanceToBottom);
    }

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
    /// Sets the horizontal position of the control relative to its parent's left border.
    /// </summary>
    /// <param name="control">The control whose position is being set.</param>
    /// <param name="distanceFromBorderLeft">The distance, in pixels, from the left border of the parent control.</param>
    /// <remarks>
    /// This method positions the control by calculating the new X-coordinate as the difference between the parent's width, 
    /// the control's width, and the specified distance from the left border.
    /// </remarks>
    public static void SetXFromParentLeft(this Control control, int distanceFromBorderLeft)
    {
        if (control.Parent == null) return;

        control.SetX(control.Parent.Width - control.Width - distanceFromBorderLeft);
    }

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
    /// Positions all child controls vertically in a waterfall layout.
    /// The first control starts at the given initial Y coordinate, and subsequent controls are spaced by the specified offset.
    /// </summary>
    /// <param name="control">The parent control containing child controls.</param>
    /// <param name="initialY">The Y coordinate to start positioning the first control.</param>
    /// <param name="offsetBetween">The vertical offset between each consecutive control.</param>
    public static void WaterFallChildControlsByY(this Control control, int initialY, int offsetBetween)
        => control.WaterFallChildControlsOfTypeByY<Control>(initialY, offsetBetween);

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

    /// <summary>
    /// Positions all child controls horizontally in a waterfall layout.
    /// The first control starts at the given initial X coordinate, and subsequent controls are spaced by the specified offset.
    /// </summary>
    /// <param name="control">The parent control containing child controls.</param>
    /// <param name="initialX">The X coordinate to start positioning the first control.</param>
    /// <param name="offsetBetween">The horizontal offset between each consecutive control.</param>
    public static void WaterFallChildControlsByX(this Control control, int initialX, int offsetBetween)
        => control.WaterFallChildControlsOfTypeByX<Control>(initialX, offsetBetween);

    /// <summary>
    /// Centralizes child controls of the specified type horizontally within the parent control,
    /// with a specified space between them.
    /// </summary>
    /// <typeparam name="T">The type of child controls to centralize.</typeparam>
    /// <param name="control">The parent control containing the child controls to be centralized.</param>
    /// <param name="spaceBetween">The space in pixels between the child controls.</param>
    public static void CentralizeWithSpacingChildrensOfTypeByX<T>(this Control control, int spaceBetween) where T : Control
    {
        var children = control.GetChildrenOfType<T>().ToList();

        if (children.Count == 0)
            return;
        else if (children.Count == 1)
        {
            children[0].HorizontalCentralize();
            return;
        }

        var totalWidth = children.Sum(c => c.Width) + spaceBetween * (children.Count - 1);
        var startingX = (control.ClientSize.Width - totalWidth) / 2;
        
        control.WaterFallChildControlsOfTypeByX<T>(startingX, spaceBetween);
    }

    /// <summary>
    /// Centralizes child controls of the specified type vertically within the parent control,
    /// with a specified space between them.
    /// </summary>
    /// <typeparam name="T">The type of child controls to centralize.</typeparam>
    /// <param name="control">The parent control containing the child controls to be centralized.</param>
    /// <param name="spaceBetween">The space in pixels between the child controls.</param>
    public static void CentralizeWithSpacingChildrensOfTypeByY<T>(this Control control, int spaceBetween) where T : Control
    {
        var children = control.GetChildrenOfType<T>().ToList();

        if (children.Count == 0)
            return;
        else if (children.Count == 1)
        {
            children[0].VerticalCentralize();
            return;
        }

        var totalHeight = children.Sum(c => c.Height) + spaceBetween * (children.Count - 1);
        var startingY = (control.ClientSize.Height - totalHeight) / 2;

        control.WaterFallChildControlsOfTypeByY<T>(startingY, spaceBetween);
    }
    #endregion

    #region [ Sizing ]

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

    /// <summary>
    /// Sets the fixed size of the control by specifying the width and height.
    /// </summary>
    /// <param name="control">The control to set the fixed size for.</param>
    /// <param name="width">The width to set for the control.</param>
    /// <param name="height">The height to set for the control.</param>
    public static void SetFixedSize(this Control control, int width, int height)
    {
        control.MinimumSize = new Size(width, height);
        control.MaximumSize = new Size(width, height);
    }

    /// <summary>
    /// Sets the fixed size of the control by specifying the size.
    /// </summary>
    /// <param name="control">The control to set the fixed size for.</param>
    /// <param name="size">The size to set for the control.</param>
    public static void SetFixedSize(this Control control, Size size)
        => control.SetFixedSize(size.Width, size.Height);

    /// <summary>
    /// Removes any fixed size constraints from the control, allowing it to resize freely.
    /// </summary>
    /// <param name="control">The control to free from fixed size constraints.</param>
    public static void FreeUpFixedSize(this Control control)
    {
        control.MinimumSize = new Size(int.MinValue, int.MinValue);
        control.MaximumSize = new Size(int.MaxValue, int.MaxValue);
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

    #region [ Control Retrieval ]
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
    /// Retrieves all immediate child controls of the specified control.
    /// </summary>
    /// <param name="control">The control whose direct children are to be retrieved.</param>
    /// <returns>An enumerable collection of immediate child controls.</returns>
    public static IEnumerable<Control> GetChildren(this Control control)
        => control.Controls.OfType<Control>();

    /// <summary>
    /// Retrieves all immediate child controls of a specified type from the given control.
    /// </summary>
    /// <typeparam name="T">The type of the child controls to retrieve.</typeparam>
    /// <param name="control">The control whose direct children of the specified type are to be retrieved.</param>
    /// <returns>An enumerable collection of immediate child controls of the specified type.</returns>
    public static IEnumerable<T> GetChildrenOfType<T>(this Control control) where T : Control
        => control.Controls.OfType<T>();

    /// <summary>
    /// Recursively retrieves all child controls of a specified type from the given control and its descendants.
    /// </summary>
    /// <typeparam name="T">The type of the child controls to retrieve.</typeparam>
    /// <param name="control">The control whose child controls and nested descendants of the specified type are to be retrieved.</param>
    /// <returns>An enumerable collection of all child controls of the specified type.</returns>
    public static IEnumerable<T> GetAllChildrenOfType<T>(this Control control) where T : Control
    {
        var children = control.GetChildrenOfType<T>().ToList();

        foreach (var child in control.GetChildren())
            children.AddRange(child.GetAllChildrenOfType<T>());

        return children;
    }
    #endregion
}