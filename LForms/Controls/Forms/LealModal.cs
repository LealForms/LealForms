using System;
using System.Drawing;
using System.Windows.Forms;

namespace LealForms.Controls.Forms;

/// <summary>
/// Represents a custom modal form inheriting from <see cref="LealForm"/>.
/// Configures specific properties for modal display, including size, location, and visibility in the taskbar.
/// </summary>
public class LealModal : LealForm
{
    /// <summary>
    /// Occurs when the modal dialog is shown.
    /// </summary>
    public event EventHandler? OnShowDialog;

    /// <summary>
    /// Occurs when the modal dialog is closed.
    /// </summary>
    public event EventHandler? OnCloseDialog;

    /// <summary>
    /// Initializes a new instance of the <see cref="LealModal"/> class with the specified size and location.
    /// </summary>
    /// <param name="startSize">The initial size of the modal form.</param>
    /// <param name="pointToScreenLocation">The screen coordinates where the modal form will be displayed.</param>
    public LealModal(Size startSize, Point pointToScreenLocation) : base(redrawOnResize: true)
    {
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;

        Size = startSize;
        ShowInTaskbar = false;
        Location = pointToScreenLocation;
    }

    /// <summary>
    /// Displays this form as a modal dialog box with no owner window and invoke <see cref="OnShowDialog"/>
    /// </summary>
    public new void ShowDialog()
    {
        OnShowDialog?.Invoke(this, new EventArgs());
        base.ShowDialog();
    }

    /// <summary>
    /// Displays this form as a modal dialog box with no owner window and invoke <see cref="OnShowDialog"/>
    /// </summary>
    public new void Show()
    {
        OnShowDialog?.Invoke(this, new EventArgs());
        ShowDialog();
    }

    /// <summary>
    /// Closes the form and invoke <see cref="OnCloseDialog"/>
    /// </summary>
    public new void Close()
    {
        OnCloseDialog?.Invoke(this, new EventArgs());
        base.Close();
    }
}