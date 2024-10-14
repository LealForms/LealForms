using LForms.Controls.Base;

namespace LForms.Controls.Mischellaneous;

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
    /// Override of the <see cref="LealBasePanel.ReDraw"/> method. 
    /// </summary>
    protected override void ReDraw() { }
}