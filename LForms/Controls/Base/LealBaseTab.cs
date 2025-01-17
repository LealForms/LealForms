using LForms.Controls.Miscellaneous;
using LForms.Controls.Panels;
using LForms.Extensions;
using System;
using System.Windows.Forms;

namespace LForms.Controls.Base;

/// <summary>
/// Represents a base class for tabs in the tab management system. Each tab has a name and 
/// is designed to automatically dock to fill its container. Ensures that the tab can only be added to a <see cref="LealTabManager"/>.
/// </summary>
public abstract class LealBaseTab : LealPanel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LealBaseTab"/> class with the specified tab name.
    /// </summary>
    /// <param name="tabName">The name of the tab.</param>
    protected LealBaseTab(string tabName)
    {
        TabName = tabName;
        Dock = DockStyle.Fill;
        ParentChanged += LealTab_ParentChanged;
    }

    /// <summary>
    /// Gets or sets the name of the tab.
    /// </summary>
    public string TabName { get; set; }

    /// <summary>
    /// Event handler triggered when the tab's parent control changes. Ensures the tab is only added to a <see cref="LealTabManager"/>.
    /// </summary>
    private void LealTab_ParentChanged(object? sender, EventArgs e)
    {
        if (Parent == null)
            return;

        if (Parent is not LealTabManager)
            Parent.Remove(this);
    }
}