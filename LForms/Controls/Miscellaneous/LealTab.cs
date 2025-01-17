using LForms.Controls.Base;
using LForms.Controls.Panels;

namespace LForms.Controls.Miscellaneous;

/// <summary>
/// Represents a standard implementation of the <see cref="LealBaseTab"/> class with a default tab name.
/// </summary>
public class LealTab : LealBaseTab
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LealTab"/> class with a default tab name of "LealTab".
    /// </summary>
    public LealTab() : base($"LealTab")
    {
    }

    /// <summary>
    /// Override of the <see cref="LealPanel.ReDraw"/> method. 
    /// </summary>
    protected override void ReDraw() { }
}