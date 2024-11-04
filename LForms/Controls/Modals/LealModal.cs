using LForms.Controls.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LForms.Controls.Modals;

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
    /// Initializes a new instance of the <see cref="LealModal"/> class with specified owner, size, and location.
    /// Configures modal form properties to exclude taskbar visibility, set border style to none, and position it manually.
    /// </summary>
    /// <param name="owner">The parent form of this modal.</param>
    /// <param name="startSize">The initial size of the modal.</param>
    /// <param name="startLocation">The initial screen location of the modal.</param>
    public LealModal(Form owner, Size startSize, Point startLocation)
    {
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;

        Owner = owner;
        Size = startSize;
        ShowInTaskbar = false;
        Location = startLocation;
        LoadComponents();
    }

    /// <summary>
    ///  Displays this form as a modal dialog box with no owner window and invoke <see cref="OnShowDialog"/>
    /// </summary>
    public new void ShowDialog()
    {
        OnShowDialog?.Invoke(this, new EventArgs());
        ShowDialog(Owner);
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