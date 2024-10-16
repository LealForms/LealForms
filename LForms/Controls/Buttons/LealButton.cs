using LForms.Controls.Base;

namespace LForms.Controls.Buttons;

/// <summary>
/// Represents a basic implementation of the <see cref="LealBaseButton"/> class.
/// </summary>
public class LealButton : LealBaseButton
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LealButton"/> class.
    /// </summary>
    public LealButton() { }

    /// <summary>
    /// Repaints the button's visual components. This method is not automatically called; you must invoke and implement it manually as needed.
    /// </summary>
    protected override void ReDraw() { }
}